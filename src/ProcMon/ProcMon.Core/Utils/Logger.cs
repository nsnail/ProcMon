using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using log4net;
using log4net.Config;

[assembly: XmlConfigurator(Watch = true, ConfigFile = "log4net.config")]

namespace ProcMon.Core.Utils
{
	public sealed class Logger
	{
		private readonly ILog _logger;

		public Logger(Type logType)
		{
			_logger = LogManager.GetLogger(logType);
#if !DEBUG
			Debug("请注意：代码运行在非调试模式下！");
#endif
		}

		static Logger()
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			GlobalContext.Properties["LogName"] =
				Path.GetFileName((Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).Location);
		}

		public static void ConsoleAlert(object text)
		{
			ConsolePrint($"{new string(' ', 50)}{text}{new string(' ', 50)}", ConsoleColor.Yellow, ConsoleColor.Red);
		}


		public void Debug(object message, [CallerMemberName] string callerName = null,
			[CallerFilePath] string callerFilePath = null,
			[CallerLineNumber] int callerLineNumber = 0)
		{
			_logger.Debug(CallerInfoMessage(message, callerName, callerFilePath, callerLineNumber));
		}


		public void Error(object message, [CallerMemberName] string callerName = null,
			[CallerFilePath] string callerFilePath = null,
			[CallerLineNumber] int callerLineNumber = 0)
		{
			_logger.Error(CallerInfoMessage(message, callerName, callerFilePath, callerLineNumber));
		}

		public void Fatal(object message, [CallerMemberName] string callerName = null,
			[CallerFilePath] string callerFilePath = null,
			[CallerLineNumber] int callerLineNumber = 0)
		{
			_logger.Fatal(CallerInfoMessage(message, callerName, callerFilePath, callerLineNumber));
		}


		public void Info(object message, [CallerMemberName] string callerName = null,
			[CallerFilePath] string callerFilePath = null,
			[CallerLineNumber] int callerLineNumber = 0)
		{
			_logger.Info(CallerInfoMessage(message, callerName, callerFilePath, callerLineNumber));
		}

		public void Warn(object message, [CallerMemberName] string callerName = null,
			[CallerFilePath] string callerFilePath = null,
			[CallerLineNumber] int callerLineNumber = 0)
		{
			_logger.Warn(CallerInfoMessage(message, callerName, callerFilePath, callerLineNumber));
		}

		private static string CallerInfoMessage(object message, string callerName, string callerFilePath,
			int callerLineNumber)
		{
			return $"[{callerName}@{Path.GetFileName(callerFilePath)}:{callerLineNumber}] {message}";
		}


		private static void ConsolePrint(object text, ConsoleColor foreColor, ConsoleColor backColor)
		{
			Console.ForegroundColor = foreColor;
			Console.BackgroundColor = backColor;
			Console.WriteLine(text);
			Console.ResetColor();
		}
	}
}