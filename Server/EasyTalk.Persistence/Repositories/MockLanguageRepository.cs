using EasyTalk.Application.Interfaces.Repositories;
using EasyTalks.Domain.Entities;

namespace EasyTalk.Persistence.Repositories
{
    public class MockLanguageRepository : ILanguageRepository
    {
        private readonly List<Language> _languages;

        public MockLanguageRepository()
        {
            _languages = new();
        }

        public async Task<Language> CreateLanguage(string languageCode, CancellationToken cancellationToken)
        {
            var language = new Language
            {
                Code = languageCode
            };

            _languages.Add(language);

            return language;
        }

        public async Task<bool> DeleteLanguage(string languageCode, CancellationToken cancellationToken)
        {
            var language = await GetLanguage(languageCode, cancellationToken);

            if (language != null)
            {
                _languages.Remove(language);

                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Language>> GetLanguages(int offset, int limit, CancellationToken cancellationToken)
        {
            return _languages
                .Skip(offset)
                .Take(limit)
                .ToList();
        }

        public async Task<Language?> GetLanguage(string languageCode, CancellationToken cancellationToken)
        {
            return _languages.FirstOrDefault(l => l.Code == languageCode);
        }
    }
}
