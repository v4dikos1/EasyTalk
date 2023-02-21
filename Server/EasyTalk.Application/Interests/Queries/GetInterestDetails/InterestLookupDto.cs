using AutoMapper;
using EasyTalk.Application.Common.Mappings;
using EasyTalks.Domain.Entities;

namespace EasyTalk.Application.Interests.Queries.GetInterestDetails
{
    public class InterestLookupDto : IMapWith<Interest>
    {
        /// <summary>
        /// Id интереса
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название интереса
        /// </summary>
        public string Name { get; set; } = String.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Interest, InterestLookupDto>();
        }
    }
}
