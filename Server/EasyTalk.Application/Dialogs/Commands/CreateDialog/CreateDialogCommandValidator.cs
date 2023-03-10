using FluentValidation;

namespace EasyTalk.Application.Dialogs.Commands.CreateDialog
{
    public class CreateDialogCommandValidator : AbstractValidator<CreateDialogCommand>
    {
        public CreateDialogCommandValidator()
        {
            RuleFor(c => c.Users).NotEmpty().WithMessage("Users field is required!");
        }
    }
}
