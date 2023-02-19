using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyTalk.Application.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand>
    {
        private readonly IEasyTalkDbContext _context;

        public UpdateLanguageCommandHandler(IEasyTalkDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = await _context.Languages.FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

            if (language == null)
            {
                throw new NotFoundException(nameof(Language), request.Id);
            }

            language.Name = request.NewName;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
