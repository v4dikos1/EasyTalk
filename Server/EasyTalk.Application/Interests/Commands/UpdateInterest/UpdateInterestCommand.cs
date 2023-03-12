using MediatR;

namespace EasyTalk.Application.Interests.Commands.UpdateInterest
{
    public class UpdateInterestCommand : IRequest
    {
        /// <summary>
        /// Id обнволяемого интереса
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Новое название интереса
        /// </summary>
        public string NewName { get; set; } = String.Empty;
    }
}
