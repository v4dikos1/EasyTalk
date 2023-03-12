using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalks.Domain.Entities;

namespace EasyTalk.Application.Messages
{
    public class MessageViewModel : IMapWith<Message>
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

        /// <summary>
        /// Вложения
        /// </summary>
        public List<Guid> Attachments { get; set; } = new();

        /// <summary>
        /// Диалог, которому принадлежит сообщение
        /// </summary>
        public Guid DialogId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Message, MessageViewModel>()
                .ForMember(m => m.Attachments,
                    opt => opt.MapFrom(a => a.Attachments
                        .ConvertAll(t => t.Id)));
        }
    }
}
