using FluentValidation;

namespace EasyTalk.Application.Translator.Commands
{
    partial class TranslateCommandValidator : AbstractValidator<TranslateCommand>
    {
        public TranslateCommandValidator()
        {
            RuleFor(c => c.TargetLanguageCode).NotEmpty().WithMessage("TargetLanguageCode is required!");
            RuleFor(c => c.Expression).NotEmpty().WithMessage("Expression is required!");
        }
    }
}
