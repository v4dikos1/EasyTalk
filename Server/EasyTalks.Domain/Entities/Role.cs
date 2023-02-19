namespace EasyTalks.Domain.Entities
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public class Role
    {
        public Guid Id { get; set; }
        
        /// <summary>
        /// Название роли
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Пользователи, принадлежащие данной роли
        /// </summary>
        public List<User> Users { get; set; } = new();
    }
}
