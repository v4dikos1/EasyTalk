using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Pictures.Commands.DeletePicture
{
    public class DeletePictureCommandHandler : IRequestHandler<DeletePictureCommand>
    {
        private readonly IEasyTalkDbContext _dbContext;
        private readonly IFileService _fileService;

        public DeletePictureCommandHandler(IEasyTalkDbContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task Handle(DeletePictureCommand request, CancellationToken cancellationToken)
        {
            var picture = await _dbContext.Pictures.FindAsync(request.Id, cancellationToken);

            if (picture == null)
            {
                throw new NotFoundException(nameof(Picture), request.Id);
            }

            if (_fileService.DeleteFile(picture.Path))
            {
                _dbContext.Pictures.Remove(picture);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
