using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalk.Application.Interests.Commands.UpdateInterest;
using EasyTalk.Application.Languages.Commands.UpdateLanguage;

namespace EasyTalk.WebApi.Models
{
    public class UpdateInterestDto : IMapWith<UpdateInterestCommand>
    {
        /// <summary>
        /// Id изменяемого интереса
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Новое название
        /// </summary>
        public string NewName { get; set; } = String.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateInterestDto, UpdateInterestCommand>();
        }
    }
}
