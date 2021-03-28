using System;

namespace ProcMon.Core.Utils
{
	public interface IConsoleProgram
	{
		void Run();
		UnhandledExceptionEventHandler UnhandledExceptionProcess();
	}
}