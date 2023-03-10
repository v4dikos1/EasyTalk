using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Dialogs.Commands.CreateDialog
{
    public class CreateDialogCommandHandler : IRequestHandler<CreateDialogCommand>
    {
        private readonly IEasyTalkDbContext _dbContext;

        public CreateDialogCommandHandler(IEasyTalkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CreateDialogCommand request, CancellationToken cancellationToken)
        {
            var users = new List<User>();

            foreach (var userId in request.Users)
            {
                var user = await _dbContext.Users.FindAsync(userId, cancellationToken);

                if (user == null)
                {
                    throw new NotFoundException(nameof(User), userId);
                }

                users.Add(user);
            }

            var dialog = new Dialog
            {
                Id = Guid.NewGuid(),
                Users = users
            };

            await _dbContext.Dialogs.AddAsync(dialog, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
