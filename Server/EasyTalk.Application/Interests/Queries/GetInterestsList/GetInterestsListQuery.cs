using MediatR;

namespace EasyTalk.Application.Interests.Queries.GetInterestsList
{
    public class GetInterestsListQuery : IRequest<InterestsListViewModel>
    {
        /// <summary>
        /// Лимит, ограничение
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Смещение
        /// </summary>
        public int Offset { get; set; }
    }
}
