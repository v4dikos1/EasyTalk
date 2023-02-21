using FluentValidation;

namespace EasyTalk.Application.Interests.Queries.GetInterestsList
{
    public class InterestsListValidator : AbstractValidator<GetInterestsListQuery>
    {
        public InterestsListValidator()
        {
            RuleFor(i => i.Offset).GreaterThanOrEqualTo(0);
            RuleFor(i => i.Limit).GreaterThanOrEqualTo(1);
        }
    }
}
