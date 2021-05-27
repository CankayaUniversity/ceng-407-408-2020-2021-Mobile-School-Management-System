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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MobilOkulProc.WebAPI.Helpers;
using MobilOkulProc.WebAPI.Services;
using AutoMapper;
using MobilOkulProc.WebAPI.Models;
using MobilOkulProc.Entities.Concrete;

namespace MobilOkulProc.WebAPI
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }
        //private readonly string key = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(x=> x.EnableEndpointRouting= false)
                .AddViewOptions(opt=> opt.HtmlHelperOptions.ClientValidationEnabled = true)
                .AddNewtonsoftJson(opt=> opt.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddCors();
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // configure strongly typed settings objects
            var appSettingsSection = _configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetById(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddScoped<IUserService, UserService>();
            //services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            //services.AddScoped<IUserService, UserService>();
            var connStr = _configuration.GetConnectionString("sqlDatabase");
            services.AddDbContext<MobilOkulContext>(opt => opt.UseSqlServer(connStr));
            services.AddSwaggerDocument();

           // services.AddAuthentication(x =>
           //{
           //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
           //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           //}).AddJwtBearer(x =>
           //{
           //    x.RequireHttpsMetadata = false;
           //    x.SaveToken = true;
           //    x.TokenValidationParameters = new TokenValidationParameters
           //    {
           //        ValidateIssuerSigningKey = true,
           //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
           //        ValidateIssuer = false,
           //        ValidateAudience = false
           //    };
           //});
           // services.AddSingleton<IJWTAuthenticationManager>(new JwtAuthenticationManager(key));



        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MobilOkulContext dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            dataContext.Database.Migrate();
            //Middleware nedir?
            //app.UseCors(x => x
            //   .AllowAnyOrigin()
            //   .AllowAnyMethod()
            //   .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseRouting();
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
            //app.UseMiddleware<JwtMiddleware>();
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseMvc();
            
            
        }
    }
}
