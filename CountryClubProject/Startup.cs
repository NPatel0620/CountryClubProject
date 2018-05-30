using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CountryClubProject.Models;
using Newtonsoft.Json.Serialization;

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
            //These are not part of .NET Core... they are seperate libraries that must be installed via a program called NuGet.
            //Right click on your Project -> Manage NuGet Packages
            //Configuration.GetConnectionString("AdventureWorks2016");

            
            string countryClubConnectionString = Configuration.GetConnectionString("CountryClub");

            //using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
            //using Microsoft.EntityFrameworkCore;
            //using Microsoft.AspNetCore.Identity;
            services.AddDbContext<CountryClubDbContext>(opt => opt.UseSqlServer(countryClubConnectionString));

            services.AddIdentity<CountryClubUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<CountryClubDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().AddJsonOptions( o =>
            {
                o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddTransient((x) => { return new EmailService(Configuration["SendGridKey"]); });
            services.AddTransient((x) => { return new Braintree.BraintreeGateway(
                                                      Configuration["BraintreeEnvironment"],
                                                      Configuration["BraintreeMerchantId"],
                                                      Configuration["BraintreePublicKey"],
                                                      Configuration["BraintreePrivateKey"]);
          
            });
            services.AddTransient((x) =>
            {
                SmartyStreets.ClientBuilder builder = new SmartyStreets.ClientBuilder(Configuration["SmartyStreetsAuthId"], Configuration["SmartyStreetsAuthToken"]);
                return builder.BuildUsStreetApiClient();
            });

            services.AddTransient((x) =>
            {

                Microsoft.WindowsAzure.Storage.CloudStorageAccount account = Microsoft.WindowsAzure.Storage.CloudStorageAccount.Parse(Configuration.GetConnectionString("CountryClubBlob"));
                return account.CreateCloudBlobClient();
            });
        }


        public void ConfigureRoutes(Microsoft.AspNetCore.Routing.IRouteBuilder routes)
        {
            //HEADACHE UNLESS YOU REALLY NEED IT!!
            //additional routes always placed above the default or they will not take effect
            //routes.MapRoute("name of item", "path keyword (waits for key press)", new { controller = "name of imported controller", action = "index"})
            //routes.MapRoute("HelmetHijack", "wearing-a-helmet", new { controller = "Helmet", action = "Index" });
            routes.MapRoute(/*name:*/ "default", /*template:*/ "{controller=Home}/{action=Index}/{id?}");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CountryClubDbContext db)
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

            //This instructs my app to use cookies for tracking SignIn and SignOut status....needs to be done manually
            //Page will not respond when logged in or out by the user.(Gives them the base app site or it breaks)
            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc(ConfigureRoutes);

            //right click generate class in new file
            db.Initialize();
        }
    }
}
