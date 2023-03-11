using FluentValidation;

namespace EasyTalk.Application.Messages.Commands.UpdateMessage
{
    public class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
    {
        public UpdateMessageCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty().WithMessage("UserId is required!");
            RuleFor(c => c.MessageId).NotEmpty().WithMessage("MessageId is required!");
        }
    }
}
