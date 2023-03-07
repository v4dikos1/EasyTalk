using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EasyTalk.Application.Common.Services
{
    public class AuthOptions
    {
        public const string Issuer = "MyAuthServer";
        public const string Audience = "MyAuthClient";
        private const string Key = "da8hccz7nc876da<dak9za";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new(Encoding.UTF8.GetBytes(Key));
    }
}
