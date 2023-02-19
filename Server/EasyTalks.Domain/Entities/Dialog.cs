namespace EasyTalks.Domain.Entities
{
    /// <summary>
    /// Диалог
    /// </summary>
    public class Dialog
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = String.Empty;

        /// <summary>
        /// Пользователи, участвующие в диалоге
        /// </summary>
        public List<User> Users { get; set; } = new();
    }
}
