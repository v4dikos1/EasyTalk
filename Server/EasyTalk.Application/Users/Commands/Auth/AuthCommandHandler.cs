using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EasyTalk.Application.Users.Commands.Auth
{
    public class AuthCommandHandler : IRequestHandler<AuthCommand, string>
    {
        private readonly IEasyTalkDbContext _dbContext;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public AuthCommandHandler(IEasyTalkDbContext dbContext, IPasswordService passwordService, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var userByEmail = 
                await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Login, cancellationToken);

            var userByUsername =
                await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Login, cancellationToken);

            if (userByEmail == null && userByUsername == null)
            {
                throw new NotFoundException(nameof(User), request.Login);
            }

            var user = userByUsername ?? userByEmail;

            if (!_passwordService.VerifyPasswordHash(request.Password, user!.PasswordHash, user.PasswordSalt))
            {
                throw new InvalidLoginOrPasswordException();
            }

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username)
            };

            var token = _tokenService.CreateToken(user, claims);

            return token;
        }
    }
}
