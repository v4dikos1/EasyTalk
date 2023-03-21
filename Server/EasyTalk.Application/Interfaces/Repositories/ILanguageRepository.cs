using EasyTalk.Application.Languages.Queries.GetLanguagesList;
using EasyTalks.Domain.Entities;

namespace EasyTalk.Application.Interfaces.Repositories
{
    public interface ILanguageRepository
    {
        Task<Language> CreateLanguage(string languageCode, CancellationToken cancellationToken);
        Task<bool> DeleteLanguage(string languageCode, CancellationToken cancellationToken);
        Task<IEnumerable<Language>> GetLanguages(int offset, int limit, CancellationToken cancellationToken);
        Task<Language?> GetLanguage(string languageCode, CancellationToken cancellationToken);
    }
}
