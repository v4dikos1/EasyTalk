using MediatR;

namespace EasyTalk.Application.Interests.Commands.UpdateInterest
{
    public class UpdateInterestCommand : IRequest
    {
        /// <summary>
        /// Имя обнволяемого интереса
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Новое название интереса
        /// </summary>
        public string NewName { get; set; } = string.Empty;
    }
}
