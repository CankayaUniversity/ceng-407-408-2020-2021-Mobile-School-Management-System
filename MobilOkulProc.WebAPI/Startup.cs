using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobilOkulProc.WebAPI.Helpers;
using MobilOkulProc.WebAPI.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System;
using MobilOkulProc.WebAPI.Data;
using Newtonsoft.Json.Serialization;
using MobilOkulProc.WebAPI.Models;
using Microsoft.Extensions.Hosting;

namespace MobilOkulProc.WebAPI
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
            var connStr = Configuration.GetConnectionString("sqlDatabase");
            services.AddDbContext<MobilOkulContext>(opt => opt.UseSqlServer(connStr));
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddMvc(x=> x.EnableEndpointRouting= false)
                .AddViewOptions(opt=> opt.HtmlHelperOptions.ClientValidationEnabled = true)
                .AddNewtonsoftJson(opt=> opt.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<MobilOkulContext>();
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
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
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
            });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();





            services.AddSwaggerDocument(); 
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MobilOkulContext dataContext)
        {
           
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            dataContext.Database.Migrate();


            app.UseHttpsRedirection();
            app.UseRouting();
            // global cors policy
            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(x => x.MapControllers());





            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseMvc();
            
            
        }
    }
}
