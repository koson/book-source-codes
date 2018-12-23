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
using Repository.Core;

namespace Repository
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");
            builder.AddEnvironmentVariables();
            IConfiguration Configuration = builder.Build();
            AppSettings.ConnectionString = Configuration["Data:DefaultConnection:ConnectionString"];
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFrameworkSqlServer();

            // Uncomment the following code for DI
            //services.AddEntityFrameworkSqlServer()
            //        .AddDbContext<AppDbContext>(options => options.UseSqlServer(AppSettings.ConnectionString));
            //services.AddScoped<ICustomerRepository, CustomerRepository>();
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
