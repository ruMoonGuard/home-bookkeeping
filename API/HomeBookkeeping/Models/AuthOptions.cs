using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HomeBookkeeping.Models
{
    public class AuthOptions
    {
        public string ISSUER { get; set; }
        public string AUDIENCE { get; set; }
        public string KEY { get; set; }
        public int LIFETIME { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }
    }
}
