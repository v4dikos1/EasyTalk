using AutoMapper;
using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Languages.Queries.GetLanguageDetails
{
    public class GetLanguageDetailsQueryHandler : IRequestHandler<GetLanguageDetailsQuery, LanguageDetailsVm>
    {
        private readonly IEasyTalkDbContext _context;
        private readonly IMapper _mapper;

        public GetLanguageDetailsQueryHandler(IEasyTalkDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LanguageDetailsVm> Handle(GetLanguageDetailsQuery request, CancellationToken cancellationToken)
        {
            var language = await _context.Languages.FindAsync(request.Id, cancellationToken);

            if (language == null)
            {
                throw new NotFoundException(nameof(Language), request.Id);
            }

            return _mapper.Map<LanguageDetailsVm>(language);
        }
    }
}
