using FluentValidation;

namespace EasyTalk.Application.Users.Commands.Registration
{
    public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
    {
        public RegistrationCommandValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required!")
                .EmailAddress().WithMessage("Invalid Email!");

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("Password is required!");

            RuleFor(u => u.Username)
                .NotEmpty()
                .WithMessage("Username is required!");

            RuleFor(u => u.PhoneNumber)
                .NotEmpty()
                .WithMessage("PhoneNumber is required!");

            RuleFor(u => u.NativeLanguageCode)
                .NotEmpty()
                .WithMessage("Native language is required!");

            RuleFor(u => u.TargetLanguages)
                .NotEmpty()
                .WithMessage("Target languages are required!");

            RuleFor(u => u.Interests)
                .NotEmpty()
                .WithMessage("Interests are required!");
        }
    }
}
