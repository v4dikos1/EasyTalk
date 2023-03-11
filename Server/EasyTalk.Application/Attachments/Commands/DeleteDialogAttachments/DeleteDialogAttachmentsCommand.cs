using MediatR;

namespace EasyTalk.Application.Attachments.Commands.DeleteDialogAttachments
{
    public class DeleteDialogAttachmentsCommand : IRequest
    {
        public Guid DialogId { get; set; }
    }
}
