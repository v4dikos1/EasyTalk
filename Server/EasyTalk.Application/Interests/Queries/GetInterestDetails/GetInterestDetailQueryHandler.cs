using AutoMapper;
using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Interests.Queries.GetInterestDetails
{
    public class GetInterestDetailQueryHandler : IRequestHandler<GetInterestDetailQuery, InterestLookupDto>
    {
        private readonly IEasyTalkDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetInterestDetailQueryHandler(IEasyTalkDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<InterestLookupDto> Handle(GetInterestDetailQuery request, CancellationToken cancellationToken)
        {
            var interest = await _dbContext.Interests.FindAsync(request.Id);

            if (interest == null)
            {
                throw new NotFoundException(nameof(Interest), request.Id);
            }

            return _mapper.Map<InterestLookupDto>(interest);
        }
    }
}
