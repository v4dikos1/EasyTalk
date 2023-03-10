using Microsoft.AspNetCore.Http;

namespace EasyTalk.Application.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(string additionalPath, IFormFile file, CancellationToken cancellationToken);
        bool DeleteFile(string path);
    }
}
