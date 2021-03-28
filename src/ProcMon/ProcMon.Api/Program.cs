using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ProcMon.Api
{
	public class Program
	{
		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
		}

		public static void Main(string[] args)
		{
			Process.Start("explorer", @"UI\index.html");
			CreateHostBuilder(args).Build().Run();
		}
	}
}