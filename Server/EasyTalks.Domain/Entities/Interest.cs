namespace EasyTalks.Domain.Entities
{
    /// <summary>
    /// Интересы пользователя - хобби, увлечения, род деятельности
    /// </summary>
    public class Interest
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Пользователи с данным интересом
        /// </summary>
        public List<User> Users { get; set; } = new();
    }
}
