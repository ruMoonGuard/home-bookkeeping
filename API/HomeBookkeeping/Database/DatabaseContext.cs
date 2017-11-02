using HomeBookkeeping.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeBookkeeping.Database
{
  public class DatabaseContext : IdentityDbContext<User>
  {
    public DbSet<Category> Categories { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Bill> Bills { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
  }
}
