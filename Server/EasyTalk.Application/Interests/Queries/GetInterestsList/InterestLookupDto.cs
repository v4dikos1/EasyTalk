using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalks.Domain.Entities;

namespace EasyTalk.Application.Interests.Queries.GetInterestsList
{
    public class InterestLookupDto : IMapWith<Interest>
    {
        public string Name { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Interest, InterestLookupDto>();
        }
    }
}
