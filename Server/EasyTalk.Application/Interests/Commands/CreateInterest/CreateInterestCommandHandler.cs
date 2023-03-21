using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalk.Application.Interfaces.Repositories;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Interests.Commands.CreateInterest
{
    public class CreateInterestCommandHandler : IRequestHandler<CreateInterestCommand, string>
    {
        private readonly IInterestRepository _interestRepository;

        public CreateInterestCommandHandler(IInterestRepository interestRepository)
        {
            _interestRepository = interestRepository;
        }

        public async Task<string> Handle(CreateInterestCommand request, CancellationToken cancellationToken)
        {
            if (await _interestRepository.GetInterest(request.Name, cancellationToken) != null)
            {
                throw new AlreadyExistsException(nameof(Interest), request.Name);
            }

            var interest = await _interestRepository.CreateInterest(request.Name, cancellationToken);

            return interest.Name;
        }
    }
}
