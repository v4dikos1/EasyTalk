using FluentValidation;

namespace EasyTalk.Application.Languages.Queries.GetLanguageDetails
{
    public class GetLanguageDetailsQueryValidator : AbstractValidator<GetLanguageDetailsQuery>
    {
        public GetLanguageDetailsQueryValidator()
        {
            RuleFor(c => c.Id).NotEqual(Guid.Empty);
        }
    }
}
