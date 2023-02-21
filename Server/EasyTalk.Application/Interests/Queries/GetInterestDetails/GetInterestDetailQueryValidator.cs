using FluentValidation;

namespace EasyTalk.Application.Interests.Queries.GetInterestDetails
{
    public class GetInterestDetailQueryValidator : AbstractValidator<GetInterestDetailQuery>
    {
        public GetInterestDetailQueryValidator()
        {
            RuleFor(i => i.Id).NotEqual(Guid.Empty);
        }
    }
}
