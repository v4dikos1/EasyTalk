using FluentValidation;

namespace EasyTalk.Application.Interests.Commands.DeleteInterest
{
    public class DeleteInterestCommandValidator : AbstractValidator<DeleteInterestCommand>
    {
        public DeleteInterestCommandValidator()
        {
            RuleFor(i => i.Id).NotEqual(Guid.Empty);
        }
    }
}
