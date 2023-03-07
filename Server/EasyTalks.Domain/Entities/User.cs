using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EasyTalks.Domain.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Firstname { get; set; } = string.Empty;

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Lastname { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronymic { get; set; }

        /// <summary>
        /// Информация о пользователе
        /// </summary>
        public string? Info { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Username { get; set; } = string.Empty; 

        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Пароль
        /// </summary>
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;

        /// <summary>
        /// Родной язык
        /// </summary>
        public Guid NativeLanguageId { get; set; }
        public Language? NativeLanguage { get; set; }

        /// <summary>
        /// Изучаемые языки
        /// </summary>
        public List<Language> TargetLanguages { get; set; } = new ();

        /// <summary>
        /// Интересы
        /// </summary>
        public List<Interest> Interests { get; set; } = new();

        /// <summary>
        /// Роль
        /// </summary>
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }

        /// <summary>
        /// Аватарка
        /// </summary>
        public Guid PictureId { get; set; }
        public Picture? Picture { get; set; }

        /// <summary>
        /// Диалоги, в которых участвует пользователь
        /// </summary>
        public List<Dialog> Dialogs { get; set; } = new();
    }
}
