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
                .Equal(2)
                .WithMessage("Only two people should participate in the dialogue!");
        }
    }
}
