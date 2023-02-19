using MediatR;

namespace EasyTalk.Application.Languages.Queries.GetLanguagesList
{
    public class GetLanguagesListQuery : IRequest<LanguageListVm>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
