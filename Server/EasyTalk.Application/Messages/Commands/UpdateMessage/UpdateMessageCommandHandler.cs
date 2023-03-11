using AutoMapper;
using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Messages.Commands.UpdateMessage
{
    public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand>
    {
        private readonly IEasyTalkDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateMessageCommandHandler(IEasyTalkDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _dbContext.Messages.FindAsync(request.MessageId, cancellationToken);

            if (message == null)
            {
                throw new NotFoundException(nameof(Message), request.MessageId);
            }

            if (message.SenderId != request.UserId)
            {
                throw new DialogOperationCancelledException();
            }

            if (request.Content != null)
            {
                message.Content = request.Content;
            }

            if (request.IsRead != null)
            {
                message.IsRead = (bool)request.IsRead;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
