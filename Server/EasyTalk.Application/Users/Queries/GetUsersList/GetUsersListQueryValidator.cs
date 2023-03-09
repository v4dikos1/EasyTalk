using FluentValidation;

namespace EasyTalk.Application.Users.Queries.GetUsersList
{
    public class GetUsersListQueryValidator : AbstractValidator<GetUsersListQuery>
    {
        public GetUsersListQueryValidator()
        {
            RuleFor(q => q.Offset)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Offset must be greater than or equal to zero!");

            RuleFor(q => q.Limit)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Limit must be greater than or equal to one!");
        }
    }
}
