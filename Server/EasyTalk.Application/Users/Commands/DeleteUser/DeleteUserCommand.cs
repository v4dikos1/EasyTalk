using MediatR;

namespace EasyTalk.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public Guid UserId { get; set;}
        public Guid CurrentUserId { get; set; }
    }
}
