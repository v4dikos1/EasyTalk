using AutoMapper.QueryableExtensions;
using EasyTalk.Application.Interfaces;
using EasyTalk.Application.Interfaces.Repositories;
using EasyTalk.Application.Users.Queries;
using EasyTalks.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyTalk.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IEasyTalkDbContext _dbContext;

        public UserRepository(IEasyTalkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUser(User user, CancellationToken cancellationToken)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> UpdateUser(Guid userId, User user, CancellationToken cancellationToken)
        {
            var userToUpdate = await GetUserById(userId, cancellationToken);

            if (userToUpdate != null)
            {
                userToUpdate = user;
                await _dbContext.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteUser(Guid userId, CancellationToken cancellationToken)
        {
            var userToDelete = await GetUserById(userId, cancellationToken);

            if (userToDelete != null)
            {
                _dbContext.Users.Remove(userToDelete);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        public async Task<User?> GetUserById(Guid userId, CancellationToken cancellationToken)
        {
            var response = await _dbContext.Users
                .Include("Interests")
                .Include("NativeLanguage")
                .Include("TargetLanguages")
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            return response;
        }

        public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            var response = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

            return response;
        }

        public async Task<User?> GetUserByPhoneNumber(string phoneNumber, CancellationToken cancellationToken)
        {
            var response = await _dbContext.Users.FirstOrDefaultAsync(u => u.PhoneNumber != null && u.PhoneNumber
                .Substring(1)
                .Equals(phoneNumber.Substring(1)), cancellationToken);

            return response;
        }

        public async Task<User?> GetUserByUsername(string username, CancellationToken cancellationToken)
        {
            var response = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username, cancellationToken);

            return response;
        }

        public async Task<IEnumerable<User>> GetUsers(int limit, int offset, List<string>? interestsFilter, List<string>? nativeLanguagesFilter, List<string>? targetLanguagesFilter,
            CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users
                .Include(u => u.Interests)
                .Include("NativeLanguage")
                .Include("TargetLanguages")
                .ToListAsync(cancellationToken);

            if (nativeLanguagesFilter != null)
            {
                users = users.Where(u => nativeLanguagesFilter.Contains(u.NativeLanguage.Code)).ToList();
            }

            if (interestsFilter != null)
            {
                users = users.Where(u => interestsFilter.ConvertAll(f => f.ToLower()).All(i => u.Interests.ConvertAll(x => x.Name)
                        .Contains(i)))
                    .ToList();
            }

            if (targetLanguagesFilter != null)
            {
                users = users
                    .Where(u => targetLanguagesFilter
                        .All(i => u.TargetLanguages
                            .ConvertAll(x => x.Code)
                            .Contains(i)))
                    .ToList();
            }

            users = users
                .Skip(offset)
                .Take(limit)
                .ToList();

            return users;
        }

        public async Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken)
        {
            return await _dbContext.Users.ToListAsync(cancellationToken);
        }
    }
}
