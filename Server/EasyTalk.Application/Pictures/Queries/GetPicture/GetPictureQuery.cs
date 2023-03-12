using MediatR;

namespace EasyTalk.Application.Pictures.Queries.GetPicture
{
    public class GetPictureQuery : IRequest<FileStream>
    {
        public Guid PicturesId { get; set; }
    }
}
