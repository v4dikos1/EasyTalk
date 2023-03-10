using AutoMapper;
using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Dialogs.Queries.GetDialogsList;
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
                .Select(d => new
                {
                    Id = d.Id,
                    Messages = d.Messages.Take(request.MessagesLimit).Skip(request.MessagesOffset).ToList(),
                    Users = d.Users
                })
                .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

            var result = new DialogLookupDto
            {
                Id = dialog!.Id,
                Messages = dialog.Messages,
                Users = dialog.Users.ConvertAll(u => _mapper.Map<UserDialogVm>(u))
            };

            if (dialog == null)
            {
                throw new NotFoundException(nameof(Dialog), request.Id);
            }

            if (!dialog.Users.ConvertAll(u => u.Id).Contains(request.CurrentUserId))
            {
                throw new DialogOperationCancelledException();
            }


            return result;
        }
    }
}
