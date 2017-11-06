using HomeBookkeeping.Database;
using HomeBookkeeping.Models;
using HomeBookkeeping.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeBookkeeping
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
              options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // издатель
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,

                    // потребитель
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,

                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,

                    //ValidateLifetime = true,

                };
            });

            //services.AddIdentity<User, IdentityRole>()
            //  .AddEntityFrameworkStores<DatabaseContext>();
              //.AddDefaultTokenProviders();

            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequiredLength = 6;   // минимальная длина
            //    options.Password.RequireNonAlphanumeric = true;   // требуются ли не алфавитно-цифровые символы
            //    options.Password.RequireLowercase = true; // требуются ли символы в нижнем регистре
            //    options.Password.RequireUppercase = true; // требуются ли символы в верхнем регистре
            //    options.Password.RequireDigit = true; // требуются ли цифры
            //    options.ClaimsIdentity.
            //});


            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Events = new CookieAuthenticationEvents
            //    {
            //        OnRedirectToLogin = ctx =>
            //        {
            //            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            //            return Task.FromResult(0);
            //        }
            //    };
            //    options.Cookie.HttpOnly = true;
            //});

            services.AddCors();
            services.AddMvc();

            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IBillService, BillService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "API",
                    Description = "API Documentation"
                });

                var bathPath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(bathPath, "API.xml");

                c.IncludeXmlComments(xmlPath);
                c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());

            app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"); });
        }
    }
}
