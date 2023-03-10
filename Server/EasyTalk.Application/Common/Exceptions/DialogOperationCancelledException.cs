namespace EasyTalk.Application.Common.Exceptions
{
    public class DialogOperationCancelledException : Exception
    {
        public DialogOperationCancelledException() : base("The chat can only be deleted/viewed by its participant")
        {

        }
    }
}
