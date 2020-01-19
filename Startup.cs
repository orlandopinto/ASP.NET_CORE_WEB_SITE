using ASP.NET_CORE_WEB_SITE.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ASP.NET_CORE_WEB_SITE
{
	public class Startup
	{
		public IConfiguration Configuration { get; private set; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().AddSessionStateTempDataProvider();
			services.AddControllersWithViews().AddRazorRuntimeCompilation();
			var storeAppSettings = new StoreAppSettings();
			Configuration.GetSection(@"StoreAppSettings").Bind(storeAppSettings);
			services.AddScoped(sp => sp.GetService<IOptionsSnapshot<SettingsStoreApp>>().Value);
			services.Configure<SettingsStoreApp>(Configuration);
			services.AddControllers(options =>
			{
				// requires using Microsoft.AspNetCore.Mvc.Formatters;
				options.OutputFormatters.RemoveType<StringOutputFormatter>();
				options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
			});
			services.AddSession();
			services.AddMemoryCache();
			services.AddDistributedMemoryCache();
			services.AddHttpContextAccessor();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthorization();
			app.UseSession();
			app.UseEndpoints(endpoints => { endpoints.MapControllerRoute(name: @"default", pattern: @"{controller=Home}/{action=Index}/{id?}"); });
			var configurationBuilder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile(@"appsettings.json", true, true).AddJsonFile($"appsettings.{env.EnvironmentName}.json", true).AddEnvironmentVariables();
			Configuration = configurationBuilder.Build();
			string conexion = Configuration.GetConnectionString(@"Test");
		}
	}
}
