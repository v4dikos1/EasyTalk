using MediatR;

namespace EasyTalk.Application.Languages.Commands.DeleteLanguage
{
    public class DeleteLanguageCommand : IRequest
    {
        /// <summary>
        /// Id удаляемого языка
        /// </summary>
        public Guid Id { get; set; }
    }
}
