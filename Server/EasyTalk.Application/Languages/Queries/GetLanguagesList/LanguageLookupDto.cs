using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalks.Domain.Entities;

namespace EasyTalk.Application.Languages.Queries.GetLanguagesList
{
    public class LanguageLookupDto : IMapWith<Language>
    {
        public string Code { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Language, LanguageLookupDto>();
        }
    }
}
