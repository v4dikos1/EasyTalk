using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Interests.Commands.CreateInterest
{
    public class CreateInterestCommandHandler : IRequestHandler<CreateInterestCommand, Guid>
    {
        private readonly IEasyTalkDbContext _dbContext;

        public CreateInterestCommandHandler(IEasyTalkDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateInterestCommand request, CancellationToken cancellationToken)
        {
            Interest interest = new Interest
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            await _dbContext.Interests.AddAsync(interest, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return interest.Id;
        }
    }
}
