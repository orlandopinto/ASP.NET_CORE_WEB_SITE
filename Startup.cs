using ASP.NET_CORE_WEB_SITE.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ASP.NET_CORE_WEB_SITE
{
	public class Startup
	{
		public IConfiguration Configuration { get; private set; }
		public AppSettings AppSettings;
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
			services.AddControllersWithViews().AddRazorRuntimeCompilation();
			services.Configure<AppSettings>(appSettings =>
			{
				appSettings.Colour = "blue";
				appSettings.ConnectionString = "ConnectionString";
			});
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
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthorization();
			app.UseSession();
			app.UseEndpoints(endpoints => { endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"); });
			var configurationBuilder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json", true, true).AddJsonFile($"appsettings.{env.EnvironmentName}.json", true).AddEnvironmentVariables();
			Configuration = configurationBuilder.Build();
			string conexion = Configuration.GetConnectionString("Test");
		}
	}
}
