using MediatR;

namespace EasyTalk.Application.Pictures.Commands.DeletePicture
{
    public class DeletePictureCommand : IRequest
    {
        /// <summary>
        /// Code удаляемого файла
        /// </summary>
        public Guid Id { get; set; }
    }
}
