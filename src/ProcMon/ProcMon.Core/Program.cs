using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading;
using ProcMon.Core.Domains;
using ProcMon.Core.Operations;
using ProcMon.Core.Utils;

namespace ProcMon.Core
{
	internal class Program : ConsoleProgram
	{
		private const int RESTART_INTERVALS = 300;


		//进程重启记录 
		private static readonly Dictionary<string, DateTime> _restartRecord = new();
		private readonly MonitorOperations _monitorOperations = new(Const.MONITORS_FILE);

		private readonly StatusesOperations _statusesOperations = new(Const.STATUSES_FILE);

		public override void Run()
		{
			Log.Debug($"读取配置：{Const.MONITORS_FILE}");
			var monitors = _monitorOperations.GetAll();
			Log.Debug("完成");
			if (monitors.Items != null) {
				Log.Debug("进程监控开始");
				ProcessMon(monitors.Items.Where(x => x.CheckExe).ToList());
				Log.Debug("进程监控完成");
				Log.Debug("日志监控开始");
				LogMon(monitors.Items.Where(x => x.CheckLog).ToList());
				Log.Debug("日志监控完成");
			}

			Log.Debug("休息2s");
			Thread.Sleep(2000);
		}

		private static string GetStartArgs(Process process)
		{
#pragma warning disable CA1416 // 验证平台兼容性
			using var searcher =
				new ManagementObjectSearcher("SELECT CommandLine,ExecutablePath FROM Win32_Process WHERE ProcessId = " +
											 process.Id);
			using var mbObject = searcher.Get().Cast<ManagementBaseObject>().Single();
			var commandLine = mbObject["CommandLine"].ToString();
			var executablePath = mbObject["ExecutablePath"].ToString();
			if (commandLine!.IndexOf($"\"{executablePath}\"", StringComparison.Ordinal) != 0) return "";
			var ret = commandLine[(executablePath!.Length + 3)..];
			return ret;
#pragma warning restore CA1416 // 验证平台兼容性
		}

		private void LogMon(ICollection<MonitorsDomain.MonitorsItem> items)
		{
			Mon(items, item => {
				DateTime logTime;
				Log.Debug($"获取日志文件时间：{item.LogPath}");
				try {
					logTime = File.GetLastWriteTime(item.LogPath);
					Log.Debug($"成功：{logTime}");
				} catch (Exception exp) {
					Log.Error("失败");
					Log.Error(exp);
					logTime = DateTime.MinValue;
				}


				return (DateTime.Now - logTime).TotalSeconds > item.LogTimeout
					? StatusesDomain.StatusVals.日志超时
					: StatusesDomain.StatusVals.正常;

			});
		}

		private static void Main()
		{
			for (; ; ) new Program().Run();
			// ReSharper disable once FunctionNeverReturns
		}

		private void Mon(ICollection<MonitorsDomain.MonitorsItem> items,
			Func<MonitorsDomain.MonitorsItem, StatusesDomain.StatusVals> getStatus)
		{
			var i = 0;
			foreach (var item in items) {
				Log.Debug($"{++i}/{items.Count}，{item.LogPath}");

				var status = getStatus(item);
				Log.Debug($"写入状态文件：{Const.STATUSES_FILE}");
				_statusesOperations.Set(new StatusesDomain.StatusesItem {
					Guid = item.Guid,
					Status = status
				});
				Log.Debug("完成");

				if (status == StatusesDomain.StatusVals.正常) {
					Log.Debug("状态正常，无需重启");
					continue;
				}

				Log.Warn(Enum.GetName(typeof(StatusesDomain.StatusVals), status));
				Log.Warn($"重启进程：{item.ExePath}");
				var psInfo = new ProcessStartInfo {
					FileName = item.ExePath,
					WorkingDirectory = item.WorkFolder,
					Arguments = item.StartArgs,
					UseShellExecute = true
				};
				ProcRestart(psInfo);
			}
		}

		private void ProcessMon(ICollection<MonitorsDomain.MonitorsItem> items)
		{
			Mon(items, item => {
				var procName = Path.GetFileNameWithoutExtension(item.ExePath);
				return Process.GetProcessesByName(procName)
					.Any(x => GetStartArgs(x) == item.StartArgs)
					? StatusesDomain.StatusVals.正常
					: StatusesDomain.StatusVals.进程退出;
			});
		}

		private void ProcRestart(ProcessStartInfo psInfo)
		{
			var psiKey = psInfo.FileName + psInfo.Arguments + psInfo.WorkingDirectory;
			if (_restartRecord.ContainsKey(psiKey)) {
				if ((DateTime.Now - _restartRecord[psiKey]).TotalSeconds < RESTART_INTERVALS) {
					Log.Warn($"未启动，进程重启间隔为{RESTART_INTERVALS}秒");
					return;
				}

				_restartRecord.Remove(psiKey);
			}

			foreach (
				var p in
				Process.GetProcessesByName(Path.GetFileNameWithoutExtension(psInfo.FileName))
					.Where(x => GetStartArgs(x) == psInfo.Arguments)) {
				Log.Debug($"杀进程{p.ProcessName}");
				try {
					p.Kill();
				} catch (Exception exp) {
					Log.Error("失败");
					Log.Error(exp);
				}
			}

			Log.Debug($"启动进程：{psInfo.FileName}");
			try {
				Process.Start(psInfo);
				Log.Debug("成功");
			} catch (Exception exp) {
				Log.Error("失败");
				Log.Error(exp);
			}

			_restartRecord.Add(psiKey, DateTime.Now);
		}
	}
}