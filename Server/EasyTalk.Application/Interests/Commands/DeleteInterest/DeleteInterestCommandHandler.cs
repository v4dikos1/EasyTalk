using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Interests.Commands.DeleteInterest
{
    public class DeleteInterestCommandHandler : IRequestHandler<DeleteInterestCommand>
    {
        private readonly IEasyTalkDbContext _dbContext;

        public DeleteInterestCommandHandler(IEasyTalkDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task Handle(DeleteInterestCommand request, CancellationToken cancellationToken)
        {
            var interestToDelete = await _dbContext.Interests.FindAsync(request.Id, cancellationToken);

            if (interestToDelete == null)
            {
                throw new NotFoundException(nameof(Interest), request.Id);
            }

            _dbContext.Interests.Remove(interestToDelete);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
