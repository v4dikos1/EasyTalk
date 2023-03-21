using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalk.Application.Interfaces.Repositories;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Interests.Commands.DeleteInterest
{
    public class DeleteInterestCommandHandler : IRequestHandler<DeleteInterestCommand>
    {
        private readonly IInterestRepository _interestRepository;

        public DeleteInterestCommandHandler(IInterestRepository interestRepository)
        {
            _interestRepository = interestRepository;
        }

        public async Task Handle(DeleteInterestCommand request, CancellationToken cancellationToken)
        {
            var response = await _interestRepository.DeleteInterest(request.Name, cancellationToken);

            if (response == false)
            {
                throw new NotFoundException(nameof(Interest), request.Name);
            }
        }
    }
}
