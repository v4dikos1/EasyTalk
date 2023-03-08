using MediatR;
using Microsoft.AspNetCore.Http;

namespace EasyTalk.Application.Pictures.Commands.AddPicture
{
    public class AddPictureCommand : IRequest<Guid>
    {
        /// <summary>
        /// Пользователь, которому устанавливается аватарка
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Аватарка
        /// </summary>
        public IFormFile File { get; set; } = null!;
    }
}
