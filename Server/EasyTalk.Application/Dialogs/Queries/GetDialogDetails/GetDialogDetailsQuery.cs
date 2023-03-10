using MediatR;

namespace EasyTalk.Application.Dialogs.Queries.GetDialogDetails
{
    public class GetDialogDetailsQuery : IRequest<DialogLookupDto>
    {
        public Guid Id { get; set; }
        public Guid CurrentUserId { get; set; }

        public int MessagesLimit { get; set; }
        public int MessagesOffset { get; set; }
    }
}
