namespace EasyTalk.Application.Common.Exceptions
{
    public class UserOperationCancelledException : Exception
    {
        public UserOperationCancelledException() : base("The user can only change/delete himself!")
        {

        }
    }
}
