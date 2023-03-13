namespace EasyTalk.WebApi.Models.User
{
    /// <summary>
    /// Модель данных для обновления пользователя.
    /// </summary>
    public class UserUpdateDto
    {
        /// <summary>
        /// Code обновляемого пользователя
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
        public string? NativeLanguageCode { get; set; }

        /// <summary>
        /// Новые изучаемые языки
        /// </summary>
        public List<string>? TargetLanguages { get; set; }

        /// <summary>
        /// Новые интересы
        /// </summary>
        public List<string>? Interests { get; set; }

        /// <summary>
        /// Новая аватарка
        /// </summary>
        public IFormFile? File { get; set; }
    }
}
