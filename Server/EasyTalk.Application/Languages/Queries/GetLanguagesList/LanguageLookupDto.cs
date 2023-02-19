using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalks.Domain.Entities;

namespace EasyTalk.Application.Languages.Queries.GetLanguagesList
{
    public class LanguageLookupDto : IMapWith<Language>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Language, LanguageLookupDto>();
        }
    }
}
