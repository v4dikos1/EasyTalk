using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalk.Application.Interfaces.Repositories;
using EasyTalk.Application.Pictures.Commands.AddPicture;
using EasyTalk.Application.Pictures.Commands.DeletePicture;
using EasyTalks.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EasyTalk.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IEasyTalkDbContext _dbContext;
        private readonly IPasswordService _passwordService;
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IInterestRepository _interestRepository;

        public UpdateUserCommandHandler(IEasyTalkDbContext dbContext, IPasswordService passwordService, IMediator mediator,
            IUserRepository userRepository, ILanguageRepository languageRepository, IInterestRepository interestRepository)
        {
            _dbContext = dbContext;
            _passwordService = passwordService;
            _mediator = mediator;
            _userRepository = userRepository;
            _languageRepository = languageRepository;
            _interestRepository = interestRepository;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.UserId, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            if (user.Id != request.CurrentUserId)
            {
                throw new UserOperationCancelledException();
            }

            if (request.UserName != null)
            {
                user.Username = request.UserName;
            }

            if (request.Email != null)
            {
                if (await _userRepository.GetUserByEmail(request.Email, cancellationToken) != null)
                {
                    throw new AlreadyExistsException(nameof(User), request.Email);
                }

                user.Email = request.Email;
            }

            if (request.PhoneNumber != null)
            {
                if (await _userRepository.GetUserByPhoneNumber(request.PhoneNumber, cancellationToken) != null)
                {
                    throw new AlreadyExistsException(nameof(User), request.PhoneNumber);
                }

                user.PhoneNumber = request.PhoneNumber;
            }

            if (request.Password != null)
            {
                _passwordService.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            if (request.NativeLanguageCode != null)
            {
                var lang = await _languageRepository.GetLanguage(request.NativeLanguageCode, cancellationToken);

                if (lang == null)
                {
                    throw new NotFoundException(nameof(Language), request.NativeLanguageCode);
                }

                user.NativeLanguageCode = request.NativeLanguageCode;
            }

            if (!request.TargetLanguages.IsNullOrEmpty())
            {
                List<Language> newTargetLanguages = new();
                foreach (var lang in request.TargetLanguages)
                {
                    var language = await _languageRepository.GetLanguage(lang, cancellationToken);

                    if (language == null)
                    {
                        throw new NotFoundException(nameof(Language), lang);
                    }

                    newTargetLanguages.Add(language);
                }

                user.TargetLanguages = newTargetLanguages;
            }

            if (!request.Interests.IsNullOrEmpty())
            {
                List<Interest> newInterests = new();

                foreach (var interest in request.Interests)
                {
                    var newInterest = await _interestRepository.GetInterest(interest.ToLower(), cancellationToken);

                    if (newInterest == null)
                    {
                        throw new NotFoundException(nameof(Interest), interest);
                    }

                    newInterests.Add(newInterest);
                }

                user.Interests = newInterests;
            }

            if (request.File != null)
            {
                var oldPicture =
                    await _dbContext.Pictures.FirstOrDefaultAsync(p => p.UserId == user.Id, cancellationToken);

                if (oldPicture != null)
                {
                    var deletePictureCommand = new DeletePictureCommand
                    {
                        Id = oldPicture.Id
                    };

                    await _mediator.Send(deletePictureCommand, cancellationToken);
                }

                var addPictureCommand = new AddPictureCommand
                {
                    File = request.File,
                    UserId = user.Id
                };

                var newPicture = await _mediator.Send(addPictureCommand, cancellationToken);

                user.PictureId = newPicture;
            }

            await _userRepository.UpdateUser(request.UserId, user, cancellationToken);
        }
    }
}
