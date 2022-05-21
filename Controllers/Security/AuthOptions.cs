using Microsoft.IdentityModel.Tokens;
using System.Text;
 
namespace notebook.Controllers.Secutiry
{
    public class AuthOptions
    {
        public const string ISSUER = "NotebookAuthServer"; // издатель токена
        public const string AUDIENCE = "NotebookAuthClient"; // потребитель токена
        const string KEY = "The!very!strong!password";   // ключ для шифрации
        public const int LIFETIME = 604800; // время жизни токена
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}