using EasyTalk.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading;

namespace EasyTalk.Persistence
{
    public class FIleService : IFileService
    {
        private readonly string _directoryPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory)?.ToString() ?? string.Empty, "EasyTalk.Persistence");

        public async Task<string> SaveFileAsync(string additionalPath, IFormFile file, string fileName, CancellationToken cancellationToken)
        {
            var filePath = Path.Combine("Uploads", additionalPath);

            var fullPath = Path.Combine(_directoryPath, filePath);

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            await using (var stream = new FileStream(Path.Combine(fullPath, fileName), FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
                await stream.FlushAsync(cancellationToken);
            }

            return filePath;
        }

        public bool DeleteFile(string path)
        {
            if (!path.StartsWith("Uploads"))
            {
                path = Path.Combine("Uploads", path);
            }

            var directory = Path.Combine(_directoryPath, path);

            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
                return true;
            }

            return false;
        }

        public FileStream GetFile(string path, string fileName)
        {
            path = Path.Combine(_directoryPath, path);

            var stream = File.Open(Path.Combine(path, fileName), FileMode.Open, FileAccess.Read, FileShare.Read);

            return stream;
        }
    }
}
