using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WEB_API_CORE
{
    public class AuthOptions
    {
        public const string ISSUER = "HyenaCoders";
        public const string AUDIENCE = "WebCarService";
        const string KEY = "mysupersecret_secretkey!123";
        public const int LIFETIME = 1440;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
