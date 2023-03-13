using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalk.Application.Pictures.Commands.AddPicture;
using EasyTalks.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyTalk.Application.Users.Commands.Registration
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand>
    {
        private readonly IPasswordService _passwordService;
        private readonly IEasyTalkDbContext _dbContext;
        private readonly IMediator _mediator;

        public RegistrationCommandHandler(IPasswordService passwordService, IEasyTalkDbContext dbContext, IMediator mediator)
        {
            _passwordService = passwordService;
            _dbContext = dbContext;
            _mediator = mediator;
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

            if (await _dbContext.Languages.FindAsync(request.NativeLanguageCode, cancellationToken) == null)
            {
                throw new NotFoundException(nameof(Language), request.NativeLanguageCode);
            }

            var interests = new List<Interest>();

            foreach (var interestId in request.Interests)
            {
                var interest = await _dbContext.Interests.FindAsync(interestId.ToLower(), cancellationToken);
                if (interest == null)
                {
                    throw new NotFoundException(nameof(Interest), interestId);
                }

                interests.Add(interest);
            }

            if (await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(request.Email), cancellationToken) != null)
            {
                throw new AlreadyExistsException(nameof(User), request.Email);
            }

            if (request.PhoneNumber != null && 
                await _dbContext.Users.FirstOrDefaultAsync(u => u.PhoneNumber != null && u.PhoneNumber.Substring(1).Equals(request.PhoneNumber.Substring(1)), cancellationToken) != null)
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
                NativeLanguageCode = request.NativeLanguageCode,
                TargetLanguages = targetLanguages,
                Interests = interests,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var pictureId = await _mediator.Send(new AddPictureCommand { UserId = user.Id, File = request.File }, cancellationToken);

            user.PictureId = pictureId;

            await _dbContext.SaveChangesAsync(cancellationToken);

        }
    }
}
