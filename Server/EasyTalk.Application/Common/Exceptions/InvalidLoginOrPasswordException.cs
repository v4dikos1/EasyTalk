namespace EasyTalk.Application.Common.Exceptions
{
    public class InvalidLoginOrPasswordException : Exception
    {
        public InvalidLoginOrPasswordException() : base("Invalid username or password")
        {

        }
    }
}
