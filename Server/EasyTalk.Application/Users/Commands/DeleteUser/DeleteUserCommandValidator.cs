using FluentValidation;

namespace EasyTalk.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty().WithMessage("UserId is required!");
            RuleFor(c => c.CurrentUserId).NotEmpty().WithMessage("CurrentUserId is required!");
        }
    }
}
