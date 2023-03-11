using EasyTalk.Application.Attachments.Commands.DeleteDialogAttachments;
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
        private readonly IMediator _mediator;

        public DeleteDialogCommandHandler(IEasyTalkDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
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

            var command = new DeleteDialogAttachmentsCommand
            {
                DialogId = dialog.Id
            };

            await _mediator.Send(command, cancellationToken);

            _dbContext.Dialogs.Remove(dialog);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
