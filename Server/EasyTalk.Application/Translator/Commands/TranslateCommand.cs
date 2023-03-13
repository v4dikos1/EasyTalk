using MediatR;

namespace EasyTalk.Application.Translator.Commands
{
    public class TranslateCommand : IRequest<string>
    {
        /// <summary>
        /// Текст для перевода
        /// </summary>
        public string Expression { get; set; } = string.Empty;

        /// <summary>
        /// Язык, на который переводить
        /// </summary>
        public string TargetLanguageCode { get; set; } = string.Empty;
    }
}
