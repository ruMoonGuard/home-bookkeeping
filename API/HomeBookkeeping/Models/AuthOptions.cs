using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HomeBookkeeping.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "localhost:4200"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123qwerty";   // ключ для шифрации
        public const int LIFETIME = 3; // время жизни токена - 3 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
