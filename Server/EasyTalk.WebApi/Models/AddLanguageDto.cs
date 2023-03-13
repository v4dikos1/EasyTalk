using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalk.Application.Languages.Commands.CreateLanguage;

namespace EasyTalk.WebApi.Models
{
    public class AddLanguageDto : IMapWith<CreateLanguageCommand>
    {
        /// <summary>
        /// Код языка
        /// </summary>
        public string Code { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddLanguageDto, CreateLanguageCommand>();
        }
    }
}
