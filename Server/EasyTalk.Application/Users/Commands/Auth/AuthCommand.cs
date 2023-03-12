using MediatR;

namespace EasyTalk.Application.Users.Commands.Auth
{
    /// <summary>
    /// Аутентификация/авторизация по логину
    /// </summary>
    public class AuthCommand : IRequest<string>
    {
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Юзернейм или почта
        /// </summary>
        public string Login { get; set; } = string.Empty;
    }
}
