using FluentValidation;

namespace EasyTalk.Application.Interests.Commands.CreateInterest
{
    public class CreateInterestCommandValidator : AbstractValidator<CreateInterestCommand>
    {
        public CreateInterestCommandValidator()
        {
            RuleFor(i => i.Name).NotEmpty().MaximumLength(20);
        }
    }
}
