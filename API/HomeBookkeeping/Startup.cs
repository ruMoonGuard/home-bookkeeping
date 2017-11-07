using HomeBookkeeping.Database;
using HomeBookkeeping.Models;
using HomeBookkeeping.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

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

            services.Configure<AuthOptions>(_configuration.GetSection("AuthOptions"));

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("JwtBearer", options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // издатель
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["AuthOptions:ISSUER"],

                    // потребитель
                    ValidateAudience = true,
                    ValidAudience = _configuration["AuthOptions:AUDIENCE"],

                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(_configuration["AuthOptions:KEY"]),
                    ValidateIssuerSigningKey = true,

                    ValidateLifetime = true,
                };
            });

            services.AddIdentity<User, IdentityRole>()
              .AddEntityFrameworkStores<DatabaseContext>()
              .AddDefaultTokenProviders();

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
