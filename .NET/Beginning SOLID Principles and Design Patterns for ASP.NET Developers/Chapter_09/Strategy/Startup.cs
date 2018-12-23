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

using Strategy.Core;

namespace Strategy
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var fi1 = env.WebRootFileProvider.GetFileInfo("/SourceFolder");
            AppSettings.SourceFolder = fi1.PhysicalPath;
            var fi2 = env.WebRootFileProvider.GetFileInfo("/DestinationFolder");
            AppSettings.DestinationFolder = fi2.PhysicalPath;
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
