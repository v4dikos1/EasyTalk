using EasyTalk.Application.Interests.Queries.GetInterestDetails;
using EasyTalks.Domain.Entities;

namespace EasyTalk.Application.Interests.Queries.GetInterestsList
{
    public class InterestsListViewModel
    {
        public IList<InterestLookupDto> Interests { get; set; } = null!;
    }
}
