using MediatR;

namespace EasyTalk.Application.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommand : IRequest<Guid>
    {
        public string Name { get; set; } = String.Empty;
    }
}
