using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Pictures.Queries.GetPicture
{
    public class GetPictureQueryHandler : IRequestHandler<GetPictureQuery, FileStream>
    {
        private readonly IEasyTalkDbContext _dbContext;
        private readonly IFileService _fileService;

        public GetPictureQueryHandler(IEasyTalkDbContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task<FileStream> Handle(GetPictureQuery request, CancellationToken cancellationToken)
        {
            var picture = await _dbContext.Pictures.FindAsync(request.PicturesId);

            if (picture == null)
            {
                throw new NotFoundException(nameof(Picture), request.PicturesId);
            }

            var file = _fileService.GetFile(picture.Path, picture.Id.ToString());

            return file;
        }
    }
}
