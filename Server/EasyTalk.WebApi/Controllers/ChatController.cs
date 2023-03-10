using AutoMapper;
using EasyTalk.Application.Dialogs.Commands.CreateDialog;
using EasyTalk.Application.Dialogs.Commands.DeleteDialog;
using EasyTalk.WebApi.Models.Chat;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EasyTalk.Application.Dialogs.Queries;
using EasyTalk.Application.Dialogs.Queries.GetDialogDetails;
using EasyTalk.Application.Dialogs.Queries.GetDialogsList;
using Microsoft.AspNetCore.Authorization;

namespace EasyTalk.WebApi.Controllers
{
    [ApiController]
    [ApiVersionNeutral]
    [Route("api/{version:apiVersion}/dialogs")]
    [Produces("application/json")]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ChatController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Создание диалога между пользователями
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST api/1.0/dialogs
        /// {
        ///     "Users": ["14bdaa2d-9c7b-45c1-b4d0-f6602e8fe227", "dca5e9c6-3968-4a92-af55-22ce8fbd965c"]
        /// }
        /// </remarks>
        /// <param name="request">Пользователи</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Диалог создан</response>
        /// <response code="400">Ошибки валидации</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateDialog([FromBody]CreateDialogDto request)
        {
            var command = _mapper.Map<CreateDialogCommand>(request);

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Удаление диалога между пользователями
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE api/1.0/dialogs?id=dca5e9c6-3968-4a92-af55-22ce8fbd965c
        /// </remarks>
        /// <param name="id">id удаляемого диалога</param>
        /// <returns>Возвращает пустой овтет</returns>
        /// <response code = "204">Диалог удален</response>
        /// <response code = "401">Не авторизован</response>
        /// <response code = "403">Недостаточно прав</response> 
        /// <response code="400">Ошибки валидации</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DeleteDialog(Guid id)
        {
            var currentUserId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            var command = new DeleteDialogCommand
            {
                Id = id,
                CurrentUserId = Guid.Parse(currentUserId)
            };

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Получение деталей диалога по id для авторизаванного пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET api/1.0/dialogs?id=dca5e9c6-3968-4a92-af55-22ce8fbd965c
        /// </remarks>
        /// <param name="id">id получаемого диалога</param>
        /// <param name="limit">Ограничение по количеству возвращаемых сообщений</param>
        /// <param name="offset">Смещение от начала чата</param>
        /// <returns>Возвращает участиников диалога и сообщения</returns>
        /// <response code = "200">Данные возвращены успешно</response>
        /// <response code = "401">Не авторизован</response>
        /// <response code = "403">Пользователь не является участником чата</response>
        /// <response code = "404">Диалог не найден</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(DialogLookupDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<DialogLookupDto>> GetChat(Guid id, int limit, int offset)
        {
            var currentUserId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            var query = new GetDialogDetailsQuery
            {
                Id = id,
                CurrentUserId = Guid.Parse(currentUserId),
                MessagesLimit = limit,
                MessagesOffset = offset
            };

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Получние списка диалогов для авторизованного пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET api/1.0/dialogs
        /// </remarks>
        /// <returns>Возвращает списко диалогов с участниками и последним сообщением</returns>
        /// /// <returns>Возвращает участиников диалога и сообщения</returns>
        /// <response code = "200">Данные возвращены успешно</response>
        /// <response code = "401">Не авторизован</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(DialogLookupDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<DialogListVm>> GetDialogs()
        {
            var currentUserId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            var query = new GetDialogsListQuery
            {
                UserId = Guid.Parse(currentUserId)
            };

            var response = await _mediator.Send(query);

            return Ok(response);
        }

    }
}
