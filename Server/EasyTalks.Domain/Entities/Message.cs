namespace EasyTalks.Domain.Entities
{
    /// <summary>
    /// Сообщение в диалоге
    /// </summary>
    public class Message
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
        public Message? RootMessage { get; set; } 

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
        public User Sender { get; set; } = null!;

        /// <summary>
        /// Получатель
        /// </summary>
        public Guid ReceiverId { get; set; }
        public User Receiver { get; set; } = null!;

        /// <summary>
        /// Вложения
        /// </summary>
        public List<Attachment> Attachments { get; set; } = new();

        /// <summary>
        /// Диалог, которому принадлежит сообщение
        /// </summary>
        public Guid DialogId { get; set; }
        public Dialog Dialog { get; set; } = null!;

    }
}
