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
using UnitOfWork.Core;

namespace UnitOfWork
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("appsettings.json");
            builder.AddEnvironmentVariables();
            IConfiguration Configuration = builder.Build();
            AppSettings.ConnectionString = Configuration["Data:DefaultConnection:ConnectionString"];
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFramework()
                    .AddSqlServer();
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

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);

    }
}
