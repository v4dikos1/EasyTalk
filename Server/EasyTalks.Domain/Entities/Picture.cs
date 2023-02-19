using System.ComponentModel.DataAnnotations.Schema;

namespace EasyTalks.Domain.Entities
{
    /// <summary>
    /// Аватарка пользователя
    /// </summary>
    public class Picture
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Путь до места хранения
        /// </summary>
        public string Path { get; set; } = String.Empty;

        /// <summary>
        /// Пользователь, загрузивший аватарку
        /// </summary>
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
