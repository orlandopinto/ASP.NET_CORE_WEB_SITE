using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ASP.NET_CORE_WEB_SITE
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseEnvironment("Production");
					webBuilder.UseStartup<Startup>();
				});
	}
}
