using FluentValidation;

namespace EasyTalk.Application.Users.Commands.Auth
{
    public class AuthCommandValidator : AbstractValidator<AuthCommand>
    {
        public AuthCommandValidator()
        {
            RuleFor(c => c.Login).NotEmpty().WithMessage("Login is required!");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
