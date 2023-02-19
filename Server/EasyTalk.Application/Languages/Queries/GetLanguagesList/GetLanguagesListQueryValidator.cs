using FluentValidation;

namespace EasyTalk.Application.Languages.Queries.GetLanguagesList
{
    public class GetLanguagesListQueryValidator : AbstractValidator<GetLanguagesListQuery>
    {
        public GetLanguagesListQueryValidator()
        {
            RuleFor(c => c.Offset).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(c => c.Limit).NotEmpty().GreaterThanOrEqualTo(1);
        }
    }
}
