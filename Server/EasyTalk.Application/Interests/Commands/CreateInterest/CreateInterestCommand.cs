using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Interests.Commands.CreateInterest
{
    public class CreateInterestCommand : IRequest<Guid>
    {
        /// <summary>
        /// Название интереса
        /// </summary>
        public string Name { get; set; } = String.Empty;
    }
}
