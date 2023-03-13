using FluentValidation;

namespace EasyTalk.Application.Languages.Commands.DeleteLanguage
{
    public class DeleteLanguageCommandValidator : AbstractValidator<DeleteLanguageCommand>
    {
        public DeleteLanguageCommandValidator()
        {
            RuleFor(c => c.Code).NotEqual(string.Empty);
        }
    }
}
