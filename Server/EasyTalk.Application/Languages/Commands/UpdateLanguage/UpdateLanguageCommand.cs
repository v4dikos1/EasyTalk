using MediatR;

namespace EasyTalk.Application.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommand : IRequest
    {
        public Guid Id { get; set; }
        public string NewName { get; set; } = String.Empty;
    }
}
