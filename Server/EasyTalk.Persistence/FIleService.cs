using EasyTalk.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EasyTalk.Persistence
{
    public class FIleService : IFileService
    {
        private readonly string _directoryPath = Directory.GetParent(Environment.CurrentDirectory) + "\\EasyTalk.Persistence";

        public async Task<string> SaveFileAsync(string additionalPath, IFormFile file, CancellationToken cancellationToken)
        {
            var filePath = "\\Uploads\\" + additionalPath + "\\";

            var fullPath = _directoryPath + filePath;

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
            var directory = _directoryPath + path;

            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
                return true;
            }

            return false;
        }
    }
}
