using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalk.Application.Languages.Commands.CreateLanguage;

namespace EasyTalk.WebApi.Models
{
    public class AddLanguageDto : IMapWith<CreateLanguageCommand>
    {
        /// <summary>
        /// Название добавляемого языка
        /// </summary>
        public string Name { get; set; } = String.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddLanguageDto, CreateLanguageCommand>();
        }
    }
}
