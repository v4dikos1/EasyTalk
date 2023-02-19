using MediatR;

namespace EasyTalk.Application.Languages.Queries.GetLanguageDetails
{
    public class GetLanguageDetailsQuery : IRequest<LanguageDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
