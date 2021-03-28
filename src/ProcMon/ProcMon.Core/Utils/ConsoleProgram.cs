using System;

namespace ProcMon.Core.Utils
{
	public abstract class ConsoleProgram : IConsoleProgram, ILogger
	{
		protected ConsoleProgram()
		{
			Log = new Logger(GetType());
			AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionProcess();
		}


		public abstract void Run();

		public UnhandledExceptionEventHandler UnhandledExceptionProcess()
		{
			return (_, e) =>
			{
				Log.Fatal(e.ExceptionObject);
#if !DEBUG
				Environment.Exit(-1);
#endif
			};
		}

		public Logger Log { get; }
	}
}