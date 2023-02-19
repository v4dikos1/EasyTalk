using FluentValidation;

namespace EasyTalk.Application.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommandValidator : AbstractValidator<UpdateLanguageCommand>
    {
        public UpdateLanguageCommandValidator()
        {
            RuleFor(c => c.NewName).NotEmpty().MaximumLength(25);
            RuleFor(c => c.Id).NotEqual(Guid.Empty);
        }
    }
}
