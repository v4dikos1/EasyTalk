using AutoMapper;
using AutoMapper.QueryableExtensions;
using EasyTalk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyTalk.Application.Languages.Queries.GetLanguagesList
{
    public class GetLanguagesListQueryHandler : IRequestHandler<GetLanguagesListQuery, LanguageListVm>
    {
        private readonly IEasyTalkDbContext _context;
        private readonly IMapper _mapper;

        public GetLanguagesListQueryHandler(IEasyTalkDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LanguageListVm> Handle(GetLanguagesListQuery request, CancellationToken cancellationToken)
        {
            var languages = await _context.Languages
                .Skip(request.Offset)
                .Take(request.Limit)
                .ProjectTo<LanguageLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new LanguageListVm { Languages = languages };
        }
    }
}
