using MediatR;

namespace EasyTalk.Application.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommand : IRequest<string>
    {
        /// <summary>
        /// Код языка
        /// </summary>
        public string Code { get; set; } = string.Empty;
    }
}
