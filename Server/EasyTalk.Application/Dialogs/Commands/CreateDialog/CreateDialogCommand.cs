using MediatR;

namespace EasyTalk.Application.Dialogs.Commands.CreateDialog
{
    public class CreateDialogCommand : IRequest
    {
        /// <summary>
        /// Участники диалога
        /// </summary>
        public List<Guid> Users { get; set; } = null!;
    }
}
