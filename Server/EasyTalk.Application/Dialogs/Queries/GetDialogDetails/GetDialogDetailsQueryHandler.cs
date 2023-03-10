using AutoMapper;
using AutoMapper.QueryableExtensions;
using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyTalk.Application.Dialogs.Queries.GetDialogDetails
{
    public class GetDialogDetailsQueryHandler : IRequestHandler<GetDialogDetailsQuery, DialogLookupDto>
    {
        private readonly IEasyTalkDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDialogDetailsQueryHandler(IEasyTalkDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<DialogLookupDto> Handle(GetDialogDetailsQuery request, CancellationToken cancellationToken)
        {
            var dialog = await _dbContext.Dialogs
                .Include("Users")
                .Include("Messages")
                .ProjectTo<DialogLookupDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

            if (dialog == null)
            {
                throw new NotFoundException(nameof(Dialog), request.Id);
            }

            if (!dialog.Users.ConvertAll(u => u.Id).Contains(request.CurrentUserId))
            {
                throw new DialogOperationCancelledException();
            }


            return new DialogLookupDto { Id = dialog.Id, Users = dialog.Users, Messages = dialog.Messages };
        }
    }
}
