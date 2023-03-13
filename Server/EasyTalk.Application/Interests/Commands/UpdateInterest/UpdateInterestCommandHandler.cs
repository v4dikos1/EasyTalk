using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Interests.Commands.UpdateInterest
{
    public class UpdateInterestCommandHandler : IRequestHandler<UpdateInterestCommand>
    {
        private readonly IEasyTalkDbContext _dbContext;

        public UpdateInterestCommandHandler(IEasyTalkDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task Handle(UpdateInterestCommand request, CancellationToken cancellationToken)
        {
            if (await _dbContext.Interests.FindAsync(request.NewName.ToLower(), cancellationToken) != null)
            {
                throw new AlreadyExistsException(nameof(Interest), request.NewName);
            }
            
            var interestToUpdate = await _dbContext.Interests.FindAsync(request.Name.ToLower(), cancellationToken);

            if (interestToUpdate == null)
            {
                throw new NotFoundException(nameof(Interest), request.Name);
            }

            interestToUpdate.Name = request.NewName.ToLower();

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
