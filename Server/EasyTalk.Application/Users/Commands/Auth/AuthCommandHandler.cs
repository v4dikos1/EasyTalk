using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using EasyTalk.Application.Interfaces.Repositories;

namespace EasyTalk.Application.Users.Commands.Auth
{
    public class AuthCommandHandler : IRequestHandler<AuthCommand, string>
    {
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public AuthCommandHandler(IPasswordService passwordService, ITokenService tokenService,
            IUserRepository userRepository)
        {
            _passwordService = passwordService;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var userByEmail = 
                await _userRepository.GetUserByEmail(request.Login, cancellationToken);

            var userByUsername =
                await _userRepository.GetUserByUsername(request.Login, cancellationToken);

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
