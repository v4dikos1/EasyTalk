using EasyTalks.Domain.Entities;

namespace EasyTalk.Application.Interfaces.Repositories
{
    public interface IInterestRepository
    {
        Task<Interest> CreateInterest(string name, CancellationToken cancellationToken);
        Task<bool> UpdateInterest(string name, string newName, CancellationToken cancellationToken);
        Task<bool> DeleteInterest(string name, CancellationToken cancellationToken);
        Task<Interest?> GetInterest(string name, CancellationToken cancellationToken);
        Task<IEnumerable<Interest>> GetInterests(int limit, int offset, CancellationToken cancellationToken);
    }
}
