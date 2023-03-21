using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalk.Application.Interfaces.Repositories;
using EasyTalk.Application.Pictures.Commands.AddPicture;
using EasyTalks.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyTalk.Application.Users.Commands.Registration
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand>
    {
        private readonly IPasswordService _passwordService;
        private readonly IMediator _mediator;
        private readonly IValidator<RegistrationCommand> _validator;
        private readonly IUserRepository _userRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IInterestRepository _interestRepository;
        private readonly IEasyTalkDbContext _dbContext;

        public RegistrationCommandHandler(IPasswordService passwordService, IMediator mediator,
            IValidator<RegistrationCommand> validator, IUserRepository userRepository, ILanguageRepository languageRepository,
            IInterestRepository interestRepository, IEasyTalkDbContext dbContext)
        {
            _passwordService = passwordService;
            _mediator = mediator;
            _validator = validator;
            _userRepository = userRepository;
            _languageRepository = languageRepository;
            _interestRepository = interestRepository;
            _dbContext = dbContext;
        }

        public async Task Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var targetLanguages = new List<Language>();

            foreach (var language in request.TargetLanguages)
            {
                var lang = await _languageRepository.GetLanguage(language, cancellationToken);
                if (lang == null)
                {
                    throw new NotFoundException(nameof(Language), language);
                }

                targetLanguages.Add(lang);
            }

            if (await _languageRepository.GetLanguage(request.NativeLanguageCode, cancellationToken) == null)
            {
                throw new NotFoundException(nameof(Language), request.NativeLanguageCode);
            }

            var interests = new List<Interest>();

            foreach (var interestId in request.Interests)
            {
                var interest = await _interestRepository.GetInterest(interestId.ToLower(), cancellationToken);
                if (interest == null)
                {
                    throw new NotFoundException(nameof(Interest), interestId);
                }

                interests.Add(interest);
            }

            if (await _userRepository.GetUserByEmail(request.Email, cancellationToken) != null)
            {
                throw new AlreadyExistsException(nameof(User), request.Email);
            }

            if (request.PhoneNumber != null && 
                await _userRepository.GetUserByPhoneNumber(request.PhoneNumber, cancellationToken) != null)
            {
                throw new AlreadyExistsException(nameof(User), request.PhoneNumber);
            }

            if (await _userRepository.GetUserByUsername(request.Username, cancellationToken) != null)
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

            await _userRepository.AddUser(user, cancellationToken);

            var pictureId = await _mediator.Send(new AddPictureCommand { UserId = user.Id, File = request.File }, cancellationToken);

            user.PictureId = pictureId;

            await _dbContext.SaveChangesAsync(cancellationToken);

        }
    }
}
