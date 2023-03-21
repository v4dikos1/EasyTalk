using MediatR;

namespace EasyTalk.Application.Languages.Commands.DeleteLanguage
{
    public class DeleteLanguageCommand : IRequest<bool>
    {
        /// <summary>
        /// Код удаляемого языка
        /// </summary>
        public string Code { get; set; } = string.Empty;
    }
}
