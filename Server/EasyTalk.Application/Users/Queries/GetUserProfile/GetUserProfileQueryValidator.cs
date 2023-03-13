using FluentValidation;

namespace EasyTalk.Application.Users.Queries.GetUserProfile
{
    public class GetUserProfileQueryValidator : AbstractValidator<GetUserProfileQuery>
    {
        public GetUserProfileQueryValidator()
        {
            RuleFor(q => q.Id).NotEmpty().WithMessage("Code if required!");
        }
    }
}
