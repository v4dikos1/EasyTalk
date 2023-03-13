using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EasyTalk.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        /// <summary>
        /// Code обновляемого пользователя
        /// </summary>
        public Guid UserId { get; set;}

        /// <summary>
        /// Текущий пользователь (тот, который пытается обновить пользователя)
        /// </summary>
        public Guid CurrentUserId { get; set; }

        public string? UserName { get; set;}

        public string? Email { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Invalid Password")]
        public string? Password { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        public string? PhoneNumber { get; set; }

        public string? NativeLanguageCode { get; set; }
        public List<string>? TargetLanguages { get; set; }
        public List<string>? Interests { get; set; }

        public IFormFile? File { get; set; }
    }
}
