namespace EasyTalk.WebApi.Models.User
{
    /// <summary>
    /// Модель данных для обновления пользователя.
    /// </summary>
    public class UserUpdateDto
    {
        /// <summary>
        /// Id обновляемого пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Новый никнейм
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Новая почта
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Новый пароль
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Новый номер телефона
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Новый родной язык
        /// </summary>
        public Guid? NativeLanguageId { get; set; }

        /// <summary>
        /// Новые изучаемые языки
        /// </summary>
        public List<Guid>? TargetLanguages { get; set; }

        /// <summary>
        /// Новые интересы
        /// </summary>
        public List<Guid>? Interests { get; set; }

        /// <summary>
        /// Новая аватарка
        /// </summary>
        public IFormFile? File { get; set; }
    }
}
