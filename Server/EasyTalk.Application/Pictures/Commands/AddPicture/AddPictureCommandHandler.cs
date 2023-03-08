using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Pictures.Commands.AddPicture
{
    public class AddPictureCommandHandler : IRequestHandler<AddPictureCommand, Guid>
    {
        private readonly IEasyTalkDbContext _dbContext;

        public AddPictureCommandHandler(IEasyTalkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(AddPictureCommand request, CancellationToken cancellationToken)
        {
            var picture = new Picture
            {
                UserId = request.UserId,
                Id = Guid.NewGuid(),
            };

            var directoryPath = Directory.GetParent(Environment.CurrentDirectory) + "\\EasyTalk.Persistence";
            var filePath ="\\Uploads\\" + request.UserId.ToString() + "\\";

            var fullPath = directoryPath + filePath;

            if (!Directory.Exists(directoryPath + filePath))
            {
                Directory.CreateDirectory(directoryPath + filePath);
            }

            picture.Path = filePath;
            await _dbContext.Pictures.AddAsync(picture, cancellationToken);

            await using (var stream = new FileStream(Path.Combine(fullPath, request.File.FileName), FileMode.Create))
            {
                await request.File.CopyToAsync(stream, cancellationToken);
                await stream.FlushAsync(cancellationToken);
            }
            

            await _dbContext.SaveChangesAsync(cancellationToken);

            return picture.Id;
        }
    }
}
