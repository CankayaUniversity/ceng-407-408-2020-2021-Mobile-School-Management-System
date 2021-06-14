using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;

namespace WebUserApp
{
    public class Startup
    {
        IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private readonly string key = "Bu benim uzun string deðerim";
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc(x => x.EnableEndpointRouting = false)
                .AddViewOptions(opt => opt.HtmlHelperOptions.ClientValidationEnabled = true)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddCors();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddRazorPages();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader());

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(configureRoutes =>
            {
                configureRoutes.MapRoute(name: "default", template: "{controller=Login}/{action=Login}/{id?}");
            });

        }
    }
}
