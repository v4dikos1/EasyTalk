using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, string>
    {
        private readonly IEasyTalkDbContext _context;

        public CreateLanguageCommandHandler(IEasyTalkDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = new Language
            {
                Code = request.Code
            };

            await _context.Languages.AddAsync(language, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return language.Code;
        }
    }
}
