using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CountryClubProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }


        public void ConfigureRoutes(Microsoft.AspNetCore.Routing.IRouteBuilder routes)
        {
            //HEADACHE UNLESS YOU REALLY NEED IT!!
            //additional routes always placed above the default or they will not take effect
            //routes.MapRoute("name of item", "path keyword (waits for key press)", new { controller = "name of imported controller", action = "index"})

            routes.MapRoute(/*name:*/ "default", /*template:*/ "{controller=Home}/{action=Index}/{id?}");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //js comes into the debugger, slows down runtime a little
                app.UseBrowserLink();
                //error message basic for user so they cannot hack
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(ConfigureRoutes);
        }
    }
}
