﻿using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalk.Application.Pictures.Commands.DeletePicture;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IEasyTalkDbContext _dbContext;
        private readonly IMediator _mediator;

        public DeleteUserCommandHandler(IEasyTalkDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            if (user.Id != request.CurrentUserId)
            {
                throw new UserOperationCancelledException();
            }

            if (user.PictureId != null)
            {
                var command = new DeletePictureCommand
                {
                    Id = (Guid)user.PictureId
                };

                await _mediator.Send(command, cancellationToken);
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
