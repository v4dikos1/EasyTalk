using EasyTalk.Application.Messages;
using EasyTalks.Domain.Entities;

namespace EasyTalk.Application.Dialogs.Queries.GetDialogsList
{
    /// <summary>
    /// Модель для отображения списка диалогов
    /// </summary>
    public class DialogVm 
    {
        /// <summary>
        /// id диалога
        /// </summary>
        public Guid DialogId { get; set; }

        /// <summary>
        /// Пользователь, с которым ведется диалог
        /// </summary>
        public UserDialogVm User { get; set; } = null!;

        /// <summary>
        /// Последнее сообщение в чате
        /// </summary>
        public MessageDialogViewModel? LastMessage { get; set; }
    }
}
