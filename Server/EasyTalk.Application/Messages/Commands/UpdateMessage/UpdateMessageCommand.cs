using MediatR;

namespace EasyTalk.Application.Messages.Commands.UpdateMessage
{
    public class UpdateMessageCommand : IRequest
    {
        public Guid MessageId { get; set; }
        public Guid UserId {get; set; }

        public string? Content { get; set; }
        public bool? IsRead { get; set; }
    }
}
