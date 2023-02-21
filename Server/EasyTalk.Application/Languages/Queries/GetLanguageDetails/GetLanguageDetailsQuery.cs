using MediatR;

namespace EasyTalk.Application.Languages.Queries.GetLanguageDetails
{
    public class GetLanguageDetailsQuery : IRequest<LanguageDetailsVm>
    {
        /// <summary>
        /// Id получаемого языка
        /// </summary>
        public Guid Id { get; set; }
    }
}
