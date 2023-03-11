using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalk.Application.Messages.Commands.CreateMessage;

namespace EasyTalk.WebApi.Models.Chat
{
    public class CreateMessageViewModel : IMapWith<CreateMessageCommand>
    {
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
        public IList<IFormFile>? Attachments { get; set; }

        /// <summary>
        /// Диалог, которому принадлежит сообщение
        /// </summary>
        public Guid DialogId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateMessageViewModel, CreateMessageCommand>();
        }
    }
}
