using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Pictures.Commands.AddPicture
{
    public class AddPictureCommandHandler : IRequestHandler<AddPictureCommand, Guid>
    {
        private readonly IEasyTalkDbContext _dbContext;
        private readonly IFileService _fileService;

        public AddPictureCommandHandler(IEasyTalkDbContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task<Guid> Handle(AddPictureCommand request, CancellationToken cancellationToken)
        {
            var picture = new Picture
            {
                UserId = request.UserId,
                Id = Guid.NewGuid(),
            };

            picture.Path = await _fileService.SaveFileAsync(picture.UserId.ToString(), request.File, picture.Id.ToString(),
                cancellationToken);
            
            await _dbContext.Pictures.AddAsync(picture, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return picture.Id;
        }
    }
}
