using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalk.Application.Languages.Commands.CreateLanguage;

namespace EasyTalk.WebApi.Models
{
    public class AddLanguageDto : IMapWith<CreateLanguageCommand>
    {
        public string Name { get; set; } = String.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddLanguageDto, CreateLanguageCommand>();
        }
    }
}
