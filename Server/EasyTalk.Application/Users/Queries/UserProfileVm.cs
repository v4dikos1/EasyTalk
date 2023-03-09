using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalk.Application.Interests.Queries.GetInterestDetails;
using EasyTalk.Application.Languages.Queries.GetLanguagesList;
using EasyTalks.Domain.Entities;

namespace EasyTalk.Application.Users.Queries
{
    public class UserProfileVm : IMapWith<User>
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
        /// Родной язык
        /// </summary>
        public LanguageLookupDto NativeLanguage { get; set; } = null!;

        /// <summary>
        /// Изучаемые языки
        /// </summary>
        public List<LanguageLookupDto> TargetLanguages { get; set; } = new();

        /// <summary>
        /// Интересы
        /// </summary>
        public List<InterestLookupDto> Interests { get; set; } = new();

        /// <summary>
        /// Аватарка
        /// </summary>
        public Guid? PictureId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserProfileVm>();
        }
    }
}
