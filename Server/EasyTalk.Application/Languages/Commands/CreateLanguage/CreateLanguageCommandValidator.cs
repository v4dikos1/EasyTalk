using FluentValidation;

namespace EasyTalk.Application.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommandValidator : AbstractValidator<CreateLanguageCommand>
    {
        public CreateLanguageCommandValidator()
        {
            RuleFor(c => c.Code).NotEmpty().MaximumLength(3);
        }
    }
}
