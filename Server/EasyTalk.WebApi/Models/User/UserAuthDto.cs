using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalk.Application.Users.Commands.Auth;

namespace EasyTalk.WebApi.Models.User
{
    public class UserAuthDto : IMapWith<AuthCommand>
    {
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Юзернейм или почта
        /// </summary>
        public string Login { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserAuthDto, AuthCommand>();
        }
    }
}
