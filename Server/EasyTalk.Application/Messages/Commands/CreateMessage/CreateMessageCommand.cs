using EasyTalk.Application.Attachments;
using EasyTalks.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EasyTalk.Application.Messages.Commands.CreateMessage
{
    public class CreateMessageCommand : IRequest<MessageViewModel>
    {
        /// <summary>
        /// Отправитель
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        /// Текстовое содержимое сообщения
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// Сообщение-родитель (то сообщение, на которое пользователь отвечает)
        /// </summary>
        public Guid? RootMessageId { get; set; }

        /// <summary>
        /// Вложения
        /// </summary>
        public List<IFormFile>? Attachments { get; set; }

        /// <summary>
        /// Диалог, которому принадлежит сообщение
        /// </summary>
        public Guid DialogId { get; set; }
    }
}
