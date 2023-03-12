using EasyTalk.Application.Interfaces;
using MediatR;

namespace EasyTalk.Application.Attachments.Commands.DeleteDialogAttachments
{
    public class DeleteDialogAttachmentsCommandHandler : IRequestHandler<DeleteDialogAttachmentsCommand>
    {
        private readonly IFileService _fileService;

        public DeleteDialogAttachmentsCommandHandler(IFileService fileService)
        {
            _fileService = fileService;
        }

        public Task Handle(DeleteDialogAttachmentsCommand request, CancellationToken cancellationToken)
        {
            var path = Path.Combine("attachments", request.DialogId.ToString());
            _fileService.DeleteFile(path);

            return Task.CompletedTask;
        }
    }
}
