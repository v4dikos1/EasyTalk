using MediatR;

namespace EasyTalk.Application.Interests.Commands.DeleteInterest
{
    public class DeleteInterestCommand : IRequest
    {
        /// <summary>
        /// Id удаляемого интереса
        /// </summary>
        public Guid Id { get; set; }
    }
}
