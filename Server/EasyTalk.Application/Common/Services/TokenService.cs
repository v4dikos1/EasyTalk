using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace EasyTalk.Application.Common.Services
{
    public class TokenService : ITokenService
    {
        public string CreateToken(User user, List<Claim> claims)
        {
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha512Signature));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
