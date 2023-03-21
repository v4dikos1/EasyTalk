using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces.Repositories;
using EasyTalks.Domain.Entities;
using FluentValidation;
using MediatR;

namespace EasyTalk.Application.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, string>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IValidator<CreateLanguageCommand> _validator;

        public CreateLanguageCommandHandler(ILanguageRepository languageRepository, IValidator<CreateLanguageCommand> validator)
        {
            _languageRepository = languageRepository;
            _validator = validator;
        }

        public async Task<string> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (await _languageRepository.GetLanguage(request.Code, cancellationToken) != null)
            {
                throw new AlreadyExistsException(nameof(Language), request.Code);
            }

            var language = await _languageRepository.CreateLanguage(request.Code, cancellationToken);

            return language.Code;
        }
    }
}
