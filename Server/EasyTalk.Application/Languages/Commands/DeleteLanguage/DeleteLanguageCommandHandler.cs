using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Languages.Commands.DeleteLanguage
{
    public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand>
    {
        private readonly IEasyTalkDbContext _context;

        public DeleteLanguageCommandHandler(IEasyTalkDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = await _context.Languages.FindAsync(request.Id, cancellationToken);

            if (language == null)
            {
                throw new NotFoundException(nameof(Language), request.Id);
            }

            _context.Languages.Remove(language);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
