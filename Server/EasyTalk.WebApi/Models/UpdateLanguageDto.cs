using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalk.Application.Languages.Commands.UpdateLanguage;

namespace EasyTalk.WebApi.Models
{
    public class UpdateLanguageDto : IMapWith<UpdateLanguageCommand>
    {
        public Guid Id { get; set; }
        public string NewName { get; set; } = String.Empty;

        public void Mapping (Profile profile)
        {
            profile.CreateMap<UpdateLanguageDto, UpdateLanguageCommand>();
        }
    }
}
