using MediatR;

namespace EasyTalk.Application.Dialogs.Queries.GetDialogsList
{
    public class GetDialogsListQuery : IRequest<DialogListVm>
    {
        /// <summary>
        /// Id пользователя, диалоги которого нужно получить
        /// </summary>
        public Guid UserId { get; set; }
    }
}
