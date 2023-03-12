using FluentValidation;

namespace EasyTalk.Application.Pictures.Queries.GetPicture
{
    public class GetPictureQueryValidator : AbstractValidator<GetPictureQuery>
    {
        public GetPictureQueryValidator()
        {
            RuleFor(q => q.PicturesId).NotEmpty().WithMessage("PictureId is required!");
        }
    }
}
