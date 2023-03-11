namespace EasyTalks.Domain.Entities
{
    /// <summary>
    /// Вложение (документ)
    /// </summary>
    public class Attachment
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Место хранения
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Сообщение, в котором содержится вложение
        /// </summary>
        public Guid MessageId { get; set; }
        public Message Message { get; set; } = null!;
    }
}
