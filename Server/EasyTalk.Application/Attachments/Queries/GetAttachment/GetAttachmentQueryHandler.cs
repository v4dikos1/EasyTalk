using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Attachments.Queries.GetAttachment
{
    public class GetAttachmentQueryHandler : IRequestHandler<GetAttachmentQuery, FileStream>
    {
        private readonly IEasyTalkDbContext _dbContext;
        private readonly IFileService _fileService;

        public GetAttachmentQueryHandler(IEasyTalkDbContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task<FileStream> Handle(GetAttachmentQuery request, CancellationToken cancellationToken)
        {
            var attachment = await _dbContext.Attachments.FindAsync(request.Id, cancellationToken);

            if (attachment == null)
            {
                throw new NotFoundException(nameof(Attachment), request.Id);
            }

            var result = _fileService.GetFile(attachment.Path, attachment.Id.ToString());

            return result;
        }
    }
}
