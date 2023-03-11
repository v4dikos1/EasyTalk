using EasyTalk.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EasyTalk.Persistence
{
    public class FIleService : IFileService
    {
        private readonly string _directoryPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory)?.ToString() ?? string.Empty, "EasyTalk.Persistence");

        public async Task<string> SaveFileAsync(string additionalPath, IFormFile file, CancellationToken cancellationToken)
        {
            var filePath = Path.Combine("Uploads", additionalPath);

            var fullPath = Path.Combine(_directoryPath, filePath);

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            await using (var stream = new FileStream(Path.Combine(fullPath, file.FileName), FileMode.Create))
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
    }
}
