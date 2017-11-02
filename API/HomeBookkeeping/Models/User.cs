using Microsoft.AspNetCore.Identity;

namespace HomeBookkeeping.Models
{
  public class User : IdentityUser
  {
    public string Login { get; set; }
  }
}
