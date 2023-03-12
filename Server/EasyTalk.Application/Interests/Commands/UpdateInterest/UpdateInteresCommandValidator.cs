using FluentValidation;

namespace EasyTalk.Application.Interests.Commands.UpdateInterest
{
    public class UpdateInteresCommandValidator : AbstractValidator<UpdateInterestCommand>
    {
        public UpdateInteresCommandValidator()
        {
            RuleFor(i => i.NewName).NotEmpty().NotEqual(String.Empty);
            RuleFor(i => i.Id).NotEqual(Guid.Empty);
        }
    }
}
