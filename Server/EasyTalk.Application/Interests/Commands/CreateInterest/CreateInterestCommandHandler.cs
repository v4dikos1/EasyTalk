using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Interests.Commands.CreateInterest
{
    public class CreateInterestCommandHandler : IRequestHandler<CreateInterestCommand, string>
    {
        private readonly IEasyTalkDbContext _dbContext;

        public CreateInterestCommandHandler(IEasyTalkDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<string> Handle(CreateInterestCommand request, CancellationToken cancellationToken)
        {
            if (await _dbContext.Interests.FindAsync(request.Name) != null)
            {
                throw new AlreadyExistsException(nameof(Interest), request.Name);
            }

            Interest interest = new Interest
            {
                Name = request.Name.ToLower()
            };

            await _dbContext.Interests.AddAsync(interest, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return interest.Name;
        }
    }
}
