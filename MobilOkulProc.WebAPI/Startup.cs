using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;
using MobilOkulProc.WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace MobilOkulProc.WebAPI
{
    public class Startup
    {
        public IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(x=> x.EnableEndpointRouting= false)
                .AddViewOptions(opt=> opt.HtmlHelperOptions.ClientValidationEnabled = true)
                .AddNewtonsoftJson(opt=> opt.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddCors();


            var connStr = _configuration.GetConnectionString("sqlDatabase");
            services.AddDbContext<MobilOkulContext>(opt => opt.UseSqlServer(connStr));
            services.AddSwaggerDocument();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            //Middleware nedir?
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseMvc();
        }
    }
}
