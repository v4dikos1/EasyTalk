using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces.Repositories;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Interests.Commands.UpdateInterest
{
    public class UpdateInterestCommandHandler : IRequestHandler<UpdateInterestCommand>
    {
        private readonly IInterestRepository _interestRepository;

        public UpdateInterestCommandHandler(IInterestRepository interestRepository)
        {
            _interestRepository = interestRepository;
        }

        public async Task Handle(UpdateInterestCommand request, CancellationToken cancellationToken)
        {
            if (await _interestRepository.GetInterest(request.NewName, cancellationToken) != null)
            {
                throw new AlreadyExistsException(nameof(Interest), request.NewName);
            }

            var response = await _interestRepository.UpdateInterest(request.Name, request.NewName, cancellationToken);

            if (response == false)
            {
                throw new NotFoundException(nameof(Interest), request.Name);
            }
        }
    }
}
