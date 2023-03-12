using MediatR;

namespace EasyTalk.Application.Pictures.Commands.DeletePicture
{
    public class DeletePictureCommand : IRequest
    {
        /// <summary>
        /// Id удаляемого файла
        /// </summary>
        public Guid Id { get; set; }
    }
}
