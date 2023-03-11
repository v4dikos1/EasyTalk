using EasyTalks.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EasyTalk.Application.Attachments.Commands.CreateAttachment
{
    public class CreateAttachmentCommand : IRequest<Attachment>
    {
        /// <summary>
        /// Сообщение, в котором содержится вложение
        /// </summary>
        public Guid MessageId { get; set; }

        /// <summary>
        /// Диалог, к которому прикреплено вложение
        /// </summary>
        public Guid DialogId { get; set; }

        /// <summary>
        /// Вложение
        /// </summary>
        public IFormFile File { get; set; } = null!;
    }
}
