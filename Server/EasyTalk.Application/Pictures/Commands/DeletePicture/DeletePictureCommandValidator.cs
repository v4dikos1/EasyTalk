using FluentValidation;

namespace EasyTalk.Application.Pictures.Commands.DeletePicture
{
    public class DeletePictureCommandValidator : AbstractValidator<DeletePictureCommand>
    {
        public DeletePictureCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Picture id is required!");
        }
    }
}
