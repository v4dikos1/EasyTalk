using EasyTalks.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalk.Application.Messages.Commands.CreateMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace EasyTalk.WebApi.Hubs
{
    [Authorize]
    public class MessengerHub : Hub<IMessengerHub>
    {
        private readonly MessengerManager _messengerManager;
        private readonly IEasyTalkDbContext _dbContext;
        private readonly IMediator _mediator;

        public MessengerHub(MessengerManager messengerManager, IEasyTalkDbContext dbContext, IMediator mediator)
        {
            _messengerManager = messengerManager;
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;

            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var user = await _dbContext.Users.FindAsync(Guid.Parse(userId));

            if (user == null)
            {
                throw new NotFoundException(nameof(User), userId);
            }

            var connectionId = Context.ConnectionId;

            _messengerManager.ConnectUser(Guid.Parse(userId), connectionId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var isUserRemoved = _messengerManager.DisconnectUser(Context.ConnectionId);
            
            if (!isUserRemoved)
            {
                await base.OnDisconnectedAsync(exception);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageAsync(string senderId, string dialogId, string message)
        {
            var command = new CreateMessageCommand
            {
                Content = message,
                DialogId = Guid.Parse(dialogId),
                SenderId = Guid.Parse(senderId)
            };

            List<IFormFile>? attachments = null;
            Guid? rootMessageId = null;

            if (attachments != null) command.Attachments = attachments;
            if (rootMessageId != null) command.RootMessageId = rootMessageId;

            var response = await _mediator.Send(command);

            var receiver = _messengerManager.Users.Find(u => u.Id.Equals(response.ReceiverId));
            var sender = _messengerManager.Users.Find(u => u.Id.Equals(response.SenderId));

            if (receiver != null)
            {
                await Clients.Clients(receiver.Connections.ToList().ConvertAll(c => c.ConnectionId))
                    .SendMessageToClientAsync(Guid.Parse(dialogId), message);
            }

            if (sender != null)
            {
                await Clients.Clients(sender.Connections.ToList().ConvertAll(c => c.ConnectionId))
                    .SendMessageToClientAsync(Guid.Parse(dialogId), message);
            }
        }
    }

    public interface IMessengerHub
    {
        Task SendMessageToClientAsync(Guid dialogId, string message);
    }
}
