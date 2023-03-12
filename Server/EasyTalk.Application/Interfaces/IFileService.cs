using Microsoft.AspNetCore.Http;

namespace EasyTalk.Application.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(string additionalPath, IFormFile file, string fileName, CancellationToken cancellationToken);
        bool DeleteFile(string path);
        FileStream GetFile(string path, string fileName);
    }
}
