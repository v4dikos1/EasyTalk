﻿using AutoMapper;
using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Attachments.Commands.CreateAttachment
{
    public class CreateAttachmentCommandHandler : IRequestHandler<CreateAttachmentCommand, Attachment>
    {
        private readonly IEasyTalkDbContext _dbContext;
        private readonly IFileService _fileService;

        public CreateAttachmentCommandHandler(IEasyTalkDbContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task<Attachment> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
        {
            var message = await _dbContext.Messages.FindAsync(request.MessageId, cancellationToken);

            if (message != null)
            {
                var attachment = new Attachment
                {
                    Id = Guid.NewGuid(),
                    MessageId = request.MessageId,
                    Message = message
                };

                var additionalPath = Path.Combine("attachments", request.DialogId.ToString(), attachment.MessageId.ToString());

                attachment.Path = await _fileService.SaveFileAsync(additionalPath, request.File, attachment.Id.ToString(),
                    cancellationToken);

                await _dbContext.Attachments.AddAsync(attachment, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return attachment;
            }

            throw new NotFoundException(nameof(Message), request.MessageId);
        }
    }
}
