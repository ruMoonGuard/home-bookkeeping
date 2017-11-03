using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBookkeeping.Database;
using HomeBookkeeping.Models;
using HomeBookkeeping.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        .AddEntityFrameworkStores<DatabaseContext>();

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
      app.UseMvc();
    }
  }
}
