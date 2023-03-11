using System.Security.Claims;
using AutoMapper;
using EasyTalk.Application.Pictures.Queries.GetPicture;
using EasyTalk.Application.Users.Commands.Auth;
using EasyTalk.Application.Users.Commands.DeleteUser;
using EasyTalk.Application.Users.Commands.Registration;
using EasyTalk.Application.Users.Commands.UpdateUser;
using EasyTalk.Application.Users.Queries;
using EasyTalk.Application.Users.Queries.GetUserProfile;
using EasyTalk.Application.Users.Queries.GetUsersList;
using EasyTalk.WebApi.Models.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyTalk.WebApi.Controllers
{
    [ApiController]
    [Route("api/{version:apiVersion}/users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /api/1.0/users
        /// {
        ///     "Username": "v4dikos",
        ///     "Email": "v4dikos@yandex.ru",
        ///     "Password": "12345",
        ///     "NativeLanguageId": "14bdaa2d-9c7b-45c1-b4d0-f6602e8fe227",
        ///     "TargetLanguages": ["dca5e9c6-3968-4a92-af55-22ce8fbd965c","f281b861-12cd-4ea3-b344-7e5eb07a5718"],
        ///     "Interests": ["dca5e9c6-3968-4a92-af55-22ce8fbd965c","f281b861-12cd-4ea3-b344-7e5eb07a5718"]
        /// }
        /// Файл берется из формы
        /// </remarks>
        /// <param name="request">Данные о пользователе</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Выполнено успешно</response>
        /// <response code="400">Ошибки валидации</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateUser([FromForm]UserRegistrationDto request)
        {
            var command = _mapper.Map<RegistrationCommand>(request);

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Аутентификация/Авторизация
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST api/1.0/users/user
        /// {
        ///     "Login": "v4dikos",
        ///     "Password": "12345"
        /// }
        /// </remarks>
        /// <param name="request">Логин (почта/юзернейм) и пароль</param>
        /// <returns>Возвращает jwt-токен</returns>
        /// <response code="200">Авторизован</response>
        /// <response code="401">Не авторизован</response>
        /// <response code="404">Пользователь не найден</response>
        [HttpPost("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Auth([FromBody] UserAuthDto request)
        {
            var command = _mapper.Map<AuthCommand>(request);

            var token = await _mediator.Send(command);

            return Ok(token);
        }

        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT api/1.0/users
        /// {
        ///     "UserId": "b897c81c-c54b-43c5-8bef-9375b7223916",
        ///     "Username": "v4dikos",
        ///     "Password": "12345"
        /// }
        /// </remarks>
        /// <remarks>
        /// Если какие-то данные не предполагается изменять, указывать их в запросе не нужно.
        /// </remarks>
        /// <param name="request">Новые данные</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// /// <response code="204">Пользователь успешно обновлен</response>
        /// <response code="401">Не авторизован</response>
        /// <response code="403">Обновляемый пользователь не совпадает с авторизованным</response>
        /// <response code="404">Пользователь не найден</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateUser([FromForm] UserUpdateDto request)
        {
            var currentUserId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            var command = new UpdateUserCommand
            {
                UserId = request.UserId,
                CurrentUserId = Guid.Parse(currentUserId),
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                NativeLanguageId = request.NativeLanguageId,
                TargetLanguages = request.TargetLanguages,
                Interests = request.Interests,
                File = request.File
            };

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE api/1.0/users?id=b897c81c-c54b-43c5-8bef-9375b7223916
        /// </remarks>
        /// <param name="id">id удаляемого пользователя</param>
        /// <returns>Возращает пустой ответ</returns>
        /// <response code="204">Пользователь успешно удален</response>
        /// <response code="401">Не авторизован</response>
        /// <response code="403">Удяляемый пользователь не совпадает с авторизованным</response>
        /// <response code="404">Пользователь не найден</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var currentUserId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            var command = new DeleteUserCommand
            {
                UserId = id,
                CurrentUserId = Guid.Parse(currentUserId)
            };

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Получение информации о пользователе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET api/1.0/users?id=b897c81c-c54b-43c5-8bef-9375b7223916
        /// </remarks>
        /// <param name="id">Id пользователя</param>
        /// <returns>Возврщает модель пользователя (UserProfileVm)</returns>
        /// <response code="200">Пользователь найден</response>
        /// <response code="404">Пользователь не найден</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserProfileVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserProfileVm>> GetUser(Guid id)
        {
            var query = new GetUserProfileQuery
            {
                Id = id
            };

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Получение информации об авторизованном пользователе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET api/1.0/users
        /// </remarks>
        /// <remarks>Authorization scheme - Bearer</remarks>
        /// <returns>Возврщает модель пользователя (UserProfileVm)</returns>
        /// <response code="200">Пользователь найден</response>
        /// <response code="401">Не авторизован</response>
        /// <response code="404">Пользователь не найден</response>
        [HttpGet]
        [ProducesResponseType(typeof(UserProfileVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<ActionResult<UserProfileVm>> GetUser()
        {
            var currentUserId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            var query = new GetUserProfileQuery
            {
                Id = Guid.Parse(currentUserId)
            };

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Получение списка пользователей
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET api/1.0/users?offset=0&amp;limit=2
        /// </remarks>
        /// <param name="limit">Ограничение возвращаемых пользователей</param>
        /// <param name="offset">Смещение от начала</param>
        /// <param name="request">Фильтры</param>
        /// <returns>Возвращает список пользователей по заданным фильтрам и ограничениям</returns>
        /// <response code="200">Выполнено успешно</response>
        [HttpGet("{limit}/{offset}")]
        [ProducesResponseType(typeof(UserProfileVm), StatusCodes.Status200OK)]
        public async Task<ActionResult<UsersListVm>> GetUsers(int limit, int offset, [FromQuery]GetUsersListRequest request)
        {
            var query = new GetUsersListQuery
            {
                Offset = offset,
                Limit = limit,
                InterestsFilter = request.InterestsFilter,
                NativeLanguagesFilter = request.NativeLanguagesFilter,
                TargetLanguagesFilter = request.TargetLanguagesFilter
            };

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Скачивание аватарки пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET api/1.0/users/picture/b897c81c-c54b-43c5-8bef-9375b7223916
        /// </remarks>
        /// <param name="id">id аватарки</param>
        /// <returns>Возвращает стрим файла</returns>
        /// <response code = "200" > Файл получен</response>
        [HttpGet("picture/{id}")]
        [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
        public async Task<FileStreamResult> GetUserPicture(Guid id)
        {
            var query = new GetPictureQuery
            {
                PicturesId = id
            };

            var stream = await _mediator.Send(query);

            return new FileStreamResult(stream, "application/octet-stream");
        }
    }
}
