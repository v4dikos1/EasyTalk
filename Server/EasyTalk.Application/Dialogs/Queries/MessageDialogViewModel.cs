using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalks.Domain.Entities;

namespace EasyTalk.Application.Dialogs.Queries
{
    public class MessageDialogViewModel : IMapWith<Message>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Текстовое содержимое сообщения
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// Сообщение-родитель (то сообщение, на которое пользователь отвечает)
        /// </summary>
        public Guid? RootMessageId { get; set; }

        /// <summary>
        /// Дата и время отправки
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Прочитано ли сообщение
        /// </summary>
        public Boolean IsRead { get; set; }

        /// <summary>
        /// Отправитель
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        /// Получатель
        /// </summary>
        public Guid ReceiverId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Message, MessageDialogViewModel>();
        }
    }
}
