using FluentValidation;

namespace EasyTalk.Application.Dialogs.Commands.DeleteDialog
{
    public class DeleteDialogCommandValidator : AbstractValidator<DeleteDialogCommand>
    {
        public DeleteDialogCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Code is required!");
            RuleFor(c => c.CurrentUserId).NotEmpty().WithMessage("CurrentUserId is required!");
        }
    }
}
