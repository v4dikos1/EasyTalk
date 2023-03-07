using System.Threading.Tasks;
using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EasyTalk.Application.Users.Commands.Registration
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand>
    {
        private readonly IPasswordService _passwordService;
        private readonly IEasyTalkDbContext _dbContext;
        private const string UserRoleId = "41f6f9c9-7ebf-4122-97f4-6d04e3eef312";

        public RegistrationCommandHandler(IPasswordService passwordService, IEasyTalkDbContext dbContext)
        {
            _passwordService = passwordService;
            _dbContext = dbContext;
        }

        public async Task Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var targetLanguages = new List<Language>();

            foreach (var language in request.TargetLanguages)
            {
                var lang = await _dbContext.Languages.FindAsync(language, cancellationToken);
                if (lang == null)
                {
                    throw new NotFoundException(nameof(Language), language);
                }

                targetLanguages.Add(lang);
            }

            if (await _dbContext.Languages.FindAsync(request.NativeLanguageId, cancellationToken) == null)
            {
                throw new NotFoundException(nameof(Language), request.NativeLanguageId);
            }

            var interests = new List<Interest>();

            foreach (var interestId in request.Interests)
            {
                var interest = await _dbContext.Interests.FindAsync(interestId, cancellationToken);
                if (interest == null)
                {
                    throw new NotFoundException(nameof(Interest), interestId);
                }

                interests.Add(interest);
            }

            if (request.RoleId != null && await _dbContext.Roles.FindAsync(request!.RoleId, cancellationToken) == null)
            {
                throw new NotFoundException(nameof(Role), request.RoleId);
            }

            if (await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(request.Email), cancellationToken) != null)
            {
                throw new AlreadyExistsException(nameof(User), request.Email);
            }

            if (await _dbContext.Users.FirstOrDefaultAsync(u => u.PhoneNumber.Equals(request.PhoneNumber), cancellationToken) != null)
            {
                throw new AlreadyExistsException(nameof(User), request.PhoneNumber);
            }

            if (await _dbContext.Users.FirstOrDefaultAsync(u => u.Username.Equals(request.Username), cancellationToken) != null)
            {
                throw new AlreadyExistsException(nameof(User), request.Username);
            }

            _passwordService.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Patronymic = request.Patronymic,
                Info = request.Info,
                NativeLanguageId = request.NativeLanguageId,
                TargetLanguages = targetLanguages,
                Interests = interests,
                RoleId = request.RoleId ?? Guid.Parse(UserRoleId),
                PictureId = request.PictureId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
