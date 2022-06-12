using CW_FrontEnd_rebuilt.ApiManager;
using CW_FrontEnd_rebuilt.ApiManager.general;
using CW_FrontEnd_rebuilt.ObjectModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CW_FrontEnd_rebuilt
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore();
            services.AddSession();
            services.AddHttpClient();

            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddScoped<IApiController<Character>, CharactersApiController>();
            services.AddScoped<IApiController<User>, UserApiController>();


            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseCors();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();
                endpoints.MapRazorPages();

            });
        }
    }
}
