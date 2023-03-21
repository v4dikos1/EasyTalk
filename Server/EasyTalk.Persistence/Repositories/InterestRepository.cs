using EasyTalk.Application.Interfaces;
using EasyTalk.Application.Interfaces.Repositories;
using EasyTalks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyTalk.Persistence.Repositories
{
    public class InterestRepository : IInterestRepository
    {
        private readonly IEasyTalkDbContext _dbContext;

        public InterestRepository(IEasyTalkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Interest> CreateInterest(string name, CancellationToken cancellationToken)
        {
            var interest = new Interest
            {
                Name = name.ToLower()
            };

            await _dbContext.Interests.AddAsync(interest, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return interest;
        }

        public async Task<bool> UpdateInterest(string name, string newName, CancellationToken cancellationToken)
        {
            var interest = await GetInterest(name, cancellationToken);

            if (interest != null)
            {
                interest.Name = newName;
                
                await _dbContext.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteInterest(string name, CancellationToken cancellationToken)
        {
            var interest = await GetInterest(name, cancellationToken);

            if (interest != null)
            {
                _dbContext.Interests.Remove(interest);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        public async Task<Interest?> GetInterest(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.Interests.FindAsync(name);
        }

        public async Task<IEnumerable<Interest>> GetInterests(int limit, int offset, CancellationToken cancellationToken)
        {
            var interestsList = await _dbContext.Interests
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return interestsList;
        }
    }
}
