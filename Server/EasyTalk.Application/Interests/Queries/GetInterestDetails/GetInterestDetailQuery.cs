using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Interests.Queries.GetInterestDetails
{
    public class GetInterestDetailQuery : IRequest<InterestLookupDto>
    {
        /// <summary>
        /// Id получаемого интереса
        /// </summary>
        public Guid Id { get; set; }
    }
}
