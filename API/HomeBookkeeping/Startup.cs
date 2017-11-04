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
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net;
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

            services.AddIdentity<User, IdentityRole>()
              .AddEntityFrameworkStores<DatabaseContext>()
              .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;   // минимальная длина
                options.Password.RequireNonAlphanumeric = true;   // требуются ли не алфавитно-цифровые символы
                options.Password.RequireLowercase = true; // требуются ли символы в нижнем регистре
                options.Password.RequireUppercase = true; // требуются ли символы в верхнем регистре
                options.Password.RequireDigit = true; // требуются ли цифры
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
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

                        ValidateLifetime = true,
                    };
                });
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

            services.ConfigureApplicationCookie(options =>
            {
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {
                        ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        return Task.FromResult(0);
                    }
                };
                options.Cookie.HttpOnly = true;
            });

            

            services.AddCors();
            services.AddMvc();

            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IBillService, BillService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
