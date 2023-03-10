using AutoMapper;
using AutoMapper.QueryableExtensions;
using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalk.Application.Users.Queries;
using EasyTalks.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyTalk.Application.Dialogs.Queries.GetDialogsList
{
    public class GetDialogsListQueryHandler : IRequestHandler<GetDialogsListQuery, DialogListVm>
    {
        private readonly IEasyTalkDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDialogsListQueryHandler(IEasyTalkDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<DialogListVm> Handle(GetDialogsListQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            var dialogs = await _dbContext.Dialogs
                .Include(d => d.Users)
                .Where(d => d.Users.Contains(user))
                .ToListAsync(cancellationToken);

            List<DialogVm> result = new List<DialogVm>();

            foreach (var dialog in dialogs)
            {
                result.Add(new DialogVm
                {
                    DialogId = dialog.Id,
                    LastMessage = dialog.Messages.LastOrDefault(),
                    User = _mapper.Map<UserDialogVm>(dialog.Users.FirstOrDefault(u => u.Id != user.Id))
                });
            }

            return new DialogListVm { Dialogs = result };
        }
    }
}
