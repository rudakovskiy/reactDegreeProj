using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GreenHealthApi
{
    public class AuthOptions
    {
        public const string ISSUER = "greenhealth-pharmacy"; // издатель токена
        public const string AUDIENCE = "0.0.0.0/"; // потребитель токена
        const string KEY = "mysupe334rsecr3et_secret2key1~";
        public const int LIFETIME = 60; // время жизни токена - 60 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}