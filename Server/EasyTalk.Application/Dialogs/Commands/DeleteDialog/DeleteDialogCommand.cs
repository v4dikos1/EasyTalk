using MediatR;

namespace EasyTalk.Application.Dialogs.Commands.DeleteDialog
{
    public class DeleteDialogCommand : IRequest
    {
        /// <summary>
        /// id удаляемого диалога
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Текущий пользователь
        /// </summary>
        public Guid CurrentUserId { get; set; }
    }
}
