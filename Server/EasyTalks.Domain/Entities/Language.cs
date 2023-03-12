namespace EasyTalks.Domain.Entities
{
    /// <summary>
    /// Язык
    /// </summary>
    public class Language
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Название языка
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Пользователи, изучающие данный язык 
        /// </summary>
        public List<User> Users { get; set; } = new();
    }
}
