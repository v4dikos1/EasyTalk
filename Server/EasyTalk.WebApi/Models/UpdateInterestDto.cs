using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalk.Application.Interests.Commands.UpdateInterest;

namespace EasyTalk.WebApi.Models
{
    public class UpdateInterestDto : IMapWith<UpdateInterestCommand>
    {
        /// <summary>
        /// Старое имя изменяемого интереса
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Новое название
        /// </summary>
        public string NewName { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateInterestDto, UpdateInterestCommand>();
        }
    }
}
