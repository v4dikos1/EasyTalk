using FluentValidation;

namespace EasyTalk.Application.Messages.Commands.CreateMessage
{
    public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageCommandValidator()
        {
            RuleFor(c => c.DialogId).NotEmpty().WithMessage("DialogId is required!");
            RuleFor(c => c.SenderId).NotEmpty().WithMessage("SenderId is required!");
        }
    }
}
