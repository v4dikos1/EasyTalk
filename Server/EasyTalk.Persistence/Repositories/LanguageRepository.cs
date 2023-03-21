using EasyTalk.Application.Interfaces;
using EasyTalk.Application.Interfaces.Repositories;
using EasyTalks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyTalk.Persistence.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly IEasyTalkDbContext _dbContext;

        public LanguageRepository(IEasyTalkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Language> CreateLanguage(string languageCode, CancellationToken cancellationToken)
        {
            var language = new Language
            {
                Code = languageCode
            };

            await _dbContext.Languages.AddAsync(language, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return language;
        }

        public async Task<bool> DeleteLanguage(string languageCode, CancellationToken cancellationToken)
        {
            var language = await GetLanguage(languageCode, cancellationToken);

            if (language != null)
            {
                _dbContext.Languages.Remove(language);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Language>> GetLanguages(int offset, int limit, CancellationToken cancellationToken)
        {
            var languages = await _dbContext.Languages
                .Skip(offset)
                .Take(limit)
                .ToArrayAsync(cancellationToken);

            return languages;
        }

        public async Task<Language?> GetLanguage(string languageCode, CancellationToken cancellationToken)
        {
            var language = await _dbContext.Languages.FindAsync(languageCode, cancellationToken);

            return language;
        }
    }
}
