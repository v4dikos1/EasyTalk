using FluentValidation;

namespace EasyTalk.Application.Dialogs.Queries.GetDialogsList
{
    public class GetDialogsListQueryValidator : AbstractValidator<GetDialogsListQuery>
    {
        public GetDialogsListQueryValidator()
        {
            RuleFor(q => q.UserId).NotEmpty().WithMessage("UserId is required!");
        }
    }
}
