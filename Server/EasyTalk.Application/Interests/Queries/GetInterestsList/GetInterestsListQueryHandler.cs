using AutoMapper;
using EasyTalk.Application.Interfaces.Repositories;
using MediatR;

namespace EasyTalk.Application.Interests.Queries.GetInterestsList
{
    public class GetInterestsListQueryHandler : IRequestHandler<GetInterestsListQuery, InterestsListViewModel>
    {
        private readonly IInterestRepository _interestRepository;
        private readonly IMapper _mapper;

        public GetInterestsListQueryHandler(IInterestRepository interestRepository, IMapper mapper)
        {
            _interestRepository = interestRepository;
            _mapper = mapper;
        }

        public async Task<InterestsListViewModel> Handle(GetInterestsListQuery request, CancellationToken cancellationToken)
        {
            var interestsList =
                await _interestRepository.GetInterests(request.Limit, request.Offset, cancellationToken);

            return new InterestsListViewModel { Interests = _mapper.Map<List<InterestLookupDto>>(interestsList)};
        }
    }
}
