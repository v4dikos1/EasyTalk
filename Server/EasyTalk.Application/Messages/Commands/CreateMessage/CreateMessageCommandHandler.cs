using AutoMapper;
using EasyTalk.Application.Attachments.Commands.CreateAttachment;
using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyTalk.Application.Messages.Commands.CreateMessage
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, MessageViewModel>
    {
        private readonly IEasyTalkDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateMessageCommandHandler(IEasyTalkDbContext dbContext, IMapper mapper, IMediator mediator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<MessageViewModel> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var sender = await _dbContext.Users.FindAsync(request.SenderId, cancellationToken);

            if (sender == null)
            {
                throw new NotFoundException(nameof(User), request.SenderId);
            }

            var dialog = await _dbContext.Dialogs
                .Include(d => d.Users)
                .FirstOrDefaultAsync(d => d.Id == request.DialogId, cancellationToken);

            if (dialog == null)
            {
                throw new NotFoundException(nameof(Dialog), request.DialogId);
            }

            if (request.RootMessageId != null)
            {
                var rootMessage = await _dbContext.Messages.FindAsync(request.RootMessageId);
                if (rootMessage == null)
                {
                    throw new NotFoundException(nameof(Message), request.RootMessageId);
                }
            }

            var receiver = dialog.Users.First(u => u.Id != request.SenderId);

            var message = new Message
            {
                Id = Guid.NewGuid(),
                Content = request.Content,
                Date = DateTime.UtcNow,
                IsRead = false,
                SenderId = request.SenderId,
                Sender = sender,
                ReceiverId = receiver.Id,
                Receiver = receiver,
                RootMessageId = request.RootMessageId,
                DialogId = request.DialogId,
                Dialog = dialog
            };

            await _dbContext.Messages.AddAsync(message, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var attachments = new List<Attachment>();
            if (request.Attachments != null)
            {
                foreach (var file in request.Attachments)
                {
                    var command = new CreateAttachmentCommand
                    {
                        File = file,
                        MessageId = message.Id,
                        DialogId = message.DialogId
                    };
                    var attachment = await _mediator.Send(command, cancellationToken);

                    attachments.Add(attachment);
                }
            }

            message.Attachments = attachments;
            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<MessageViewModel>(message);

            return result;
        }
    }
}
