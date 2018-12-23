using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNet.Hosting;
using Microsoft.Dnx.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Entity;
using Microsoft.Extensions.PlatformAbstractions;

using Proxy.Core;

namespace Proxy
{
    public class Startup
    {

        public Startup(IHostingEnvironment env, IApplicationEnvironment app)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(app.ApplicationBasePath);
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            AppSettings.ServiceBaseAddress = config.Get<string>("AppSettings:ServiceBaseAddress");
            AppSettings.ServiceUrl = config.Get <string>("AppSettings:ServiceUrl");
            AppSettings.LogFilePath = env.MapPath(config.Get<string>("AppSettings:LogFilePath"));
            AppSettings.ConnectionString = config.Get<string>("Data:DefaultConnection:ConnectionString");

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFramework()
                    .AddSqlServer();
            services.AddSession();
            services.AddCaching();
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

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);

    }
}
