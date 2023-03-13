using System.ComponentModel.DataAnnotations;
using EasyTalks.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EasyTalk.Application.Users.Commands.Registration
{
    public class RegistrationCommand : IRequest
    {
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
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [DataType(DataType.Password, ErrorMessage = "Invalid Password")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Родной язык
        /// </summary>
        public string NativeLanguageCode { get; set; } = string.Empty;

        /// <summary>
        /// Изучаемые языки
        /// </summary>
        public List<string> TargetLanguages { get; set; } = new();

        /// <summary>
        /// Интересы
        /// </summary>
        public List<string> Interests { get; set; } = new();

        /// <summary>
        /// Аватар
        /// </summary>
        public IFormFile File { get; set; } = null!;
    }
}
