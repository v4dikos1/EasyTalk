namespace EasyTalks.Domain.Entities
{
    /// <summary>
    /// Язык
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Название языка
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Пользователи, изучающие данный язык 
        /// </summary>
        public List<User> Users { get; set; } = new();
    }
}
