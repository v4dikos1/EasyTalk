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
        public List<User> Receivers { get; set; } = null!;

        /// <summary>
        /// Вложения
        /// </summary>
        public List<Attachment> Attachments { get; set; } = new();
    }
}
