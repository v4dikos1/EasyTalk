using FluentValidation;

namespace EasyTalk.Application.Dialogs.Queries.GetDialogDetails
{
    public class GetDialogDetailsQueryValidator : AbstractValidator<GetDialogDetailsQuery>
    {
        public GetDialogDetailsQueryValidator()
        {
            RuleFor(q => q.Id).NotEmpty().WithMessage("Id field is required!");
            RuleFor(q => q.CurrentUserId).NotEmpty().WithMessage("CurrentUserId field is required!");
        }
    }
}
