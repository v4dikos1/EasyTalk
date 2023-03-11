namespace EasyTalks.Domain.Entities
{
    /// <summary>
    /// Диалог
    /// </summary>
    public class Dialog
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Пользователи, участвующие в диалоге
        /// </summary>
        public List<User> Users { get; set; } = new();

        /// <summary>
        /// Сообщения
        /// </summary>
        public List<Message> Messages { get; set; } = new();
    }
}
