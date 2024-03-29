﻿using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalk.Application.Users.Commands.Registration;

namespace EasyTalk.WebApi.Models.User
{
    public class UserRegistrationDto : IMapWith<RegistrationCommand>
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
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Пароль
        /// </summary>
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserRegistrationDto, RegistrationCommand>();
        }
    }
}
