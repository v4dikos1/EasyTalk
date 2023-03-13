using MediatR;

namespace EasyTalk.Application.Interests.Commands.DeleteInterest
{
    public class DeleteInterestCommand : IRequest
    {
        /// <summary>
        /// Имя удаляемого интереса
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
