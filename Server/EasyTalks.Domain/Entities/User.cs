﻿namespace EasyTalks.Domain.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        public Guid Id { get; set; }

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
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;

        /// <summary>
        /// Родной язык
        /// </summary>
        public string NativeLanguageCode { get; set; } = string.Empty;
        public Language NativeLanguage { get; set; } = null!;

        /// <summary>
        /// Изучаемые языки
        /// </summary>
        public List<Language> TargetLanguages { get; set; } = new ();

        /// <summary>
        /// Интересы
        /// </summary>
        public List<Interest> Interests { get; set; } = new();

        /// <summary>
        /// Аватарка
        /// </summary>
        public Guid? PictureId { get; set; }
        public Picture? Picture { get; set; }

        /// <summary>
        /// Диалоги, в которых участвует пользователь
        /// </summary>
        public List<Dialog> Dialogs { get; set; } = new();
    }
}
