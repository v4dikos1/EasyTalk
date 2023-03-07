using System.ComponentModel.DataAnnotations;
using MediatR;

namespace EasyTalk.Application.Users.Commands.Registration
{
    public class RegistrationCommand : IRequest
    {
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
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Пароль
        /// </summary>
        [DataType(DataType.Password, ErrorMessage = "Invalid Password")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Родной язык
        /// </summary>
        public Guid NativeLanguageId { get; set; }

        /// <summary>
        /// Изучаемые языки
        /// </summary>
        public List<Guid> TargetLanguages { get; set; } = new();

        /// <summary>
        /// Интересы
        /// </summary>
        public List<Guid> Interests { get; set; } = new();

        /// <summary>
        /// Роль
        /// </summary>
        public Guid? RoleId { get; set; }

        /// <summary>
        /// Аватарка
        /// </summary>
        public Guid PictureId { get; set; }
    }
}
