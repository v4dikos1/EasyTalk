namespace EasyTalk.Application.Common.Exceptions
{
    public class DialogOperationCancelledException : Exception
    {
        public DialogOperationCancelledException() : base("The chat or message can only be deleted/viewed by its participant")
        {

        }
    }
}
