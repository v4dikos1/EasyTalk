using MediatR;

namespace EasyTalk.Application.Attachments.Queries.GetAttachment
{
    public class GetAttachmentQuery : IRequest<FileStream>
    {
        public Guid Id { get; set; }
    }
}
