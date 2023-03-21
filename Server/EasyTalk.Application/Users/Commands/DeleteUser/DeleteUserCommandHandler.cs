using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalk.Application.Interfaces.Repositories;
using EasyTalk.Application.Pictures.Commands.DeletePicture;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IMediator mediator, IUserRepository userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.UserId, cancellationToken);

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

            await _userRepository.DeleteUser(request.UserId, cancellationToken);
        }
    }
}
