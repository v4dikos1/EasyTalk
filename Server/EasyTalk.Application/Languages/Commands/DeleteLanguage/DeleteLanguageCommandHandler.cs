using EasyTalk.Application.Interfaces.Repositories;
using FluentValidation;
using MediatR;
using ValidationException = FluentValidation.ValidationException;

namespace EasyTalk.Application.Languages.Commands.DeleteLanguage
{
    public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand, bool>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IValidator<DeleteLanguageCommand> _validator;

        public DeleteLanguageCommandHandler(ILanguageRepository languageRepository, IValidator<DeleteLanguageCommand> validator)
        {
            _languageRepository = languageRepository;
            _validator = validator;
        }

        public async Task<bool> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var response = await _languageRepository.DeleteLanguage(request.Code, cancellationToken);

            return response;
        }
    }
}
