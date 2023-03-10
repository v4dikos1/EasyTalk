using AutoMapper;
using EasyTalk.Application.Dialogs.Commands.CreateDialog;
using EasyTalk.WebApi.Models.Chat;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        /// /// <response code="204">Диалог создан</response>
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
    }
}
