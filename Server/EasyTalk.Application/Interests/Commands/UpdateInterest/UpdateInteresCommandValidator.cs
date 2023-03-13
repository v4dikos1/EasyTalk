using FluentValidation;

namespace EasyTalk.Application.Interests.Commands.UpdateInterest
{
    public class UpdateInteresCommandValidator : AbstractValidator<UpdateInterestCommand>
    {
        public UpdateInteresCommandValidator()
        {
            RuleFor(i => i.NewName).NotEmpty().NotEqual(string.Empty);
            RuleFor(i => i.Name).NotEqual(string.Empty);
        }
    }
}
