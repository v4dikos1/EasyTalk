using FluentValidation;

namespace EasyTalk.Application.Pictures.Commands.AddPicture
{
    public class AddPictureCommandValidator : AbstractValidator<AddPictureCommand>
    {
        public AddPictureCommandValidator()
        {
            RuleFor(p => p.UserId)
                .NotEmpty()
                .WithMessage("UserId is required!");

            RuleFor(p => p.File)
                .NotEmpty()
                .WithMessage("File is required!");
        }
    }
}
