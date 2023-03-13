using EasyTalk.Application.Common.Exceptions;
using EasyTalk.Application.Interfaces;
using EasyTalks.Domain.Entities;
using MediatR;

namespace EasyTalk.Application.Translator.Commands
{
    public class TranslateCommandHandler : IRequestHandler<TranslateCommand, string>
    {
        private readonly ITranslateService _translateService;
        private readonly IEasyTalkDbContext _dbContext;

        public TranslateCommandHandler(ITranslateService translateService, IEasyTalkDbContext dbContext)
        {
            _translateService = translateService;
            _dbContext = dbContext;
        }

        public async Task<string> Handle(TranslateCommand request, CancellationToken cancellationToken)
        {
            if (await _dbContext.Languages.FindAsync(request.TargetLanguageCode) == null)
            {
                throw new NotFoundException(nameof(Language), request.TargetLanguageCode);
            }

            var response = await _translateService.TranslateAsync(request.Expression, request.TargetLanguageCode);

            return response;
        }
    }
}
