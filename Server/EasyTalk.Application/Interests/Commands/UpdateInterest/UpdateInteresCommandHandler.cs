using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Interests.Commands.UpdateInterest
{
    public class UpdateInteresCommandHandler : IRequestHandler<UpdateInterestCommand>
    {
        private readonly IEasyTalkDbContext _dbContext;

        public UpdateInteresCommandHandler(IEasyTalkDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task Handle(UpdateInterestCommand request, CancellationToken cancellationToken)
        {
            var interestToUpdate = await _dbContext.Interests.FindAsync(request.Id, cancellationToken);

            if (interestToUpdate == null)
            {
                throw new NotFoundException(nameof(Interest), request.Id);
            }

            interestToUpdate.Name = request.NewName;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
