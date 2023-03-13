using AutoMapper;
using AutoMapper.QueryableExtensions;
using EasyTalk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyTalk.Application.Interests.Queries.GetInterestsList
{
    public class GetInterestsListQueryHandler : IRequestHandler<GetInterestsListQuery, InterestsListViewModel>
    {
        private readonly IEasyTalkDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetInterestsListQueryHandler(IEasyTalkDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<InterestsListViewModel> Handle(GetInterestsListQuery request, CancellationToken cancellationToken)
        {
            var interestsList = await _dbContext.Interests
                .Skip(request.Offset)
                .Take(request.Limit)
                .ProjectTo<InterestLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new InterestsListViewModel { Interests = interestsList};
        }
    }
}
