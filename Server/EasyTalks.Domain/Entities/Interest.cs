namespace EasyTalks.Domain.Entities
{
    /// <summary>
    /// Интересы пользователя - хобби, увлечения, род деятельности
    /// </summary>
    public class Interest
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Пользователи с данным интересом
        /// </summary>
        public List<User> Users { get; set; } = new();
    }
}
