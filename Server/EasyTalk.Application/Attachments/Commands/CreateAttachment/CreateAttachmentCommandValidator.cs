using FluentValidation;

namespace EasyTalk.Application.Attachments.Commands.CreateAttachment
{
    public class CreateAttachmentCommandValidator : AbstractValidator<CreateAttachmentCommand>
    {
        public CreateAttachmentCommandValidator()
        {
            RuleFor(c => c.File).NotEmpty().WithMessage("File is required!");
            RuleFor(c => c.MessageId).NotEmpty().WithMessage("MessageId is required!");
        }
    }
}
