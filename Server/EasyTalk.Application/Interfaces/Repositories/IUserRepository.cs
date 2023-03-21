using EasyTalks.Domain.Entities;

namespace EasyTalk.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User user, CancellationToken cancellationToken);
        Task<bool> UpdateUser(Guid userId, User user, CancellationToken cancellationToken);
        Task<bool> DeleteUser(Guid userId, CancellationToken cancellationToken);
        Task<User?> GetUserById(Guid userId, CancellationToken cancellationToken);
        Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);
        Task<User?> GetUserByPhoneNumber(string phoneNumber, CancellationToken cancellationToken);
        Task<User?> GetUserByUsername(string username, CancellationToken cancellationToken);

        Task<IEnumerable<User>> GetUsers(int limit, int offset, List<string>? interestsFilter,
            List<string>? nativeLanguagesFilter,
            List<string>? targetLanguagesFilter, CancellationToken cancellationToken);

        Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken);
    }
}
