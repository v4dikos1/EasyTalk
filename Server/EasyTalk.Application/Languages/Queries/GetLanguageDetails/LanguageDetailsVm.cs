using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalks.Domain.Entities;

namespace EasyTalk.Application.Languages.Queries.GetLanguageDetails
{
    public class LanguageDetailsVm : IMapWith<Language>
    {
        public Guid LanguageId { get; set; }
        public string LanguageName { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Language, LanguageDetailsVm>()
                .ForMember(vm => vm.LanguageId,
                opt => opt.MapFrom(l => l.Id))
                .ForMember(vm => vm.LanguageName,
                opt => opt.MapFrom(l => l.Name));
        }
    }
}
