using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyTalk.Application.Dialogs.Commands.DeleteDialog
{
    public class DeleteDialogCommandHandler : IRequestHandler<DeleteDialogCommand>
    {
        private readonly IEasyTalkDbContext _dbContext;

        public DeleteDialogCommandHandler(IEasyTalkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteDialogCommand request, CancellationToken cancellationToken)
        {
            var dialog = await _dbContext.Dialogs
                .Include("Users")
                .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

            if (dialog == null)
            {
                throw new NotFoundException(nameof(Dialog), request.Id);
            }

            if (!dialog.Users.ConvertAll(u => u.Id).Contains(request.CurrentUserId))
            {
                throw new DialogOperationCancelledException();
            }

            _dbContext.Dialogs.Remove(dialog);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
