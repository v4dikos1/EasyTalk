using System.Security.Claims;
using EasyTalks.Domain.Entities;

namespace EasyTalk.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user, List<Claim> claims);
    }
}
