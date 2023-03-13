using FluentValidation;

namespace EasyTalk.Application.Dialogs.Queries.GetDialogDetails
{
    public class GetDialogDetailsQueryValidator : AbstractValidator<GetDialogDetailsQuery>
    {
        public GetDialogDetailsQueryValidator()
        {
            RuleFor(q => q.Id).NotEmpty().WithMessage("Code field is required!");
            RuleFor(q => q.CurrentUserId).NotEmpty().WithMessage("CurrentUserId field is required!");

            RuleFor(q => q.AttachmentsLimit)
                .GreaterThanOrEqualTo(0)
                .WithMessage("AttachmentsLimit must be greater than or equal to zero!");

            RuleFor(q => q.AttachmentsOffset)
                .GreaterThanOrEqualTo(0)
                .WithMessage("AttachmentsOffset must be greater than or equal to zero!");

            RuleFor(q => q.MessagesLimit)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MessagesLimit must be greater than or equal to zero!");

            RuleFor(q => q.MessagesOffset)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MessagesOffset must be greater than or equal to zero!");
        }
    }
}
