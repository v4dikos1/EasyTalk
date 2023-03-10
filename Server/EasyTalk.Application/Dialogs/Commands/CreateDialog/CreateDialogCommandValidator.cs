using FluentValidation;

namespace EasyTalk.Application.Dialogs.Commands.CreateDialog
{
    public class CreateDialogCommandValidator : AbstractValidator<CreateDialogCommand>
    {
        public CreateDialogCommandValidator()
        {
            RuleFor(c => c.Users)
                .NotEmpty()
                .WithMessage("Users field is required!");

            RuleFor(c => c.Users.Count)
                .GreaterThanOrEqualTo(2)
                .WithMessage("At least two people should participate in the dialogue!");
        }
    }
}
