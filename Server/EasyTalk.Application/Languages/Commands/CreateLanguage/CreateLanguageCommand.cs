using MediatR;

namespace EasyTalk.Application.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommand : IRequest<Guid>
    {
        /// <summary>
        /// Название создаваемого языка
        /// </summary>
        public string Name { get; set; } = String.Empty;
    }
}
