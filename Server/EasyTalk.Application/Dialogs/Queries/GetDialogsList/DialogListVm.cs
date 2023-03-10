namespace EasyTalk.Application.Dialogs.Queries.GetDialogsList
{
    /// <summary>
    /// Список диалогов пользователя
    /// </summary>
    public class DialogListVm
    {
        public List<DialogVm> Dialogs { get; set; } = new();
    }
}
