using AutoMapper;
using EasyTalk.Application.Languages.Commands.CreateLanguage;
using EasyTalk.Application.Languages.Commands.DeleteLanguage;
using EasyTalk.Application.Languages.Queries.GetLanguagesList;
using EasyTalk.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyTalk.WebApi.Controllers
{
    [ApiController]
    [Route("api/{version:apiVersion}/languages")]
    [Produces("application/json")]
    public class LanguageController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LanguageController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение всех языков
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /api/{apiVersion}/languages?offset=0&amp;limit=2
        /// </remarks>
        /// <param name="offset">Смещение</param>
        /// <param name="limit">Лимит</param>
        /// <returns>Возвращает список всех языков по заданным смещению и лимиту</returns>
        /// <response code="200">Выполнено успешно</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="400">Ошибки валидации</response>
        [HttpGet("{offset}/{limit}")]
        [ProducesResponseType(typeof(LanguageListVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LanguageListVm>> GetAll(int offset, int limit)
        {
            var query = new GetLanguagesListQuery
            {
                Offset = offset,
                Limit = limit
            };

            var vm = await _mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Добавление языка
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /api/1.0/languages
        /// {
        ///     "Code": "ru"
        /// }
        /// </remarks>
        /// <param name="request">Название языка</param>
        /// <returns>Возвращает код созданного языка</returns>
        /// <response code="200">Выполнено успешно</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="400">Ошибки валидации</response>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> AddLanguage([FromBody] AddLanguageDto request)
        {
            var command = _mapper.Map<CreateLanguageCommand>(request);

            var response = await _mediator.Send(command);

            return Ok(response);
        }

        /// <summary>
        /// Удаление языка
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /api/1.0/languages/ru
        /// </remarks>
        /// <param name="code">Код удаляемого языка</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Выполнено успешно</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="400">Ошибки валидации</response>
        [HttpDelete("{code}")]
        [Authorize]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteLanguage(string code)
        {
            var command = new DeleteLanguageCommand
            {
                Code = code
            };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
