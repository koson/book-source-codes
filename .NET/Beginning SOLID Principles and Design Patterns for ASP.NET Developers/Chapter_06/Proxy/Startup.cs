using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Proxy.Core;

namespace Proxy
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath);
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            AppSettings.ServiceBaseAddress = config["AppSettings:ServiceBaseAddress"];
            AppSettings.ServiceUrl = config["AppSettings:ServiceUrl"];

            var fi = env.WebRootFileProvider.GetFileInfo(config["AppSettings:LogFilePath"]);
            AppSettings.LogFilePath = fi.PhysicalPath;

            AppSettings.ConnectionString = config["Data:DefaultConnection:ConnectionString"];

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFrameworkSqlServer();
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
