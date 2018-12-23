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

using Bridge.Core;

namespace Bridge
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath);
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();
            AppSettings.ConnectionString = config["Data:DefaultConnection:ConnectionString"];
            var fi = env.WebRootFileProvider.GetFileInfo("/ErrorLogs");
            AppSettings.LogFileFolder = fi.PhysicalPath;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFrameworkSqlServer();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
