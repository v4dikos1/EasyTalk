using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, Guid>
    {
        private readonly IEasyTalkDbContext _context;

        public CreateLanguageCommandHandler(IEasyTalkDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = new Language
            {
                Name = request.Name,
                Id = Guid.NewGuid()
            };

            await _context.Languages.AddAsync(language, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return language.Id;
        }
    }
}
