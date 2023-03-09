using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Pictures.Commands.DeletePicture
{
    public class DeletePictureCommandHandler : IRequestHandler<DeletePictureCommand>
    {
        private readonly IEasyTalkDbContext _dbContext;

        public DeletePictureCommandHandler(IEasyTalkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeletePictureCommand request, CancellationToken cancellationToken)
        {
            var picture = await _dbContext.Pictures.FindAsync(request.Id, cancellationToken);

            if (picture == null)
            {
                throw new NotFoundException(nameof(Picture), request.Id);
            }

            var directoryPath = Directory.GetParent(Environment.CurrentDirectory) + "\\EasyTalk.Persistence";

            var directory = directoryPath + picture.Path;

            if (Directory.Exists(directory))
            {
                //var di = new DirectoryInfo(directory);

                //foreach (FileInfo file in di.EnumerateFiles())
                //{
                //    file.Delete();
                //}
                
                Directory.Delete(directory, true);
            }

            _dbContext.Pictures.Remove(picture);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
