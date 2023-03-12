using MediatR;

namespace EasyTalk.Application.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommand : IRequest
    {
        /// <summary>
        /// Id изменяемого языка
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Новое название
        /// </summary>
        public string NewName { get; set; } = String.Empty;
    }
}
