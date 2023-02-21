using AutoMapper;
using EasyTalk.Application.Languages.Commands.CreateLanguage;
using EasyTalk.Application.Languages.Commands.DeleteLanguage;
using EasyTalk.Application.Languages.Commands.UpdateLanguage;
using EasyTalk.Application.Languages.Queries.GetLanguageDetails;
using EasyTalk.Application.Languages.Queries.GetLanguagesList;
using EasyTalk.WebApi.Models;
using MediatR;
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
        /// Получение языка
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /api/1.0/languages/aa242kksf-sfkmfsm5345-43k345353-m3m533
        /// </remarks>
        /// <param name="id">id языка</param>
        /// <returns>Возвращает информацию о языке (id, name)</returns>
        /// <response code="200">Выполнено успешно</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="400">Ошибки валидации</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LanguageDetailsVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LanguageDetailsVm>> GetLanguage(Guid id)
        {
            var query = new GetLanguageDetailsQuery
            {
                Id = id
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
        ///     "Name": "Русский"
        /// }
        /// </remarks>
        /// <param name="request">Название языка</param>
        /// <returns>Возвращает id (guid) созданного языка</returns>
        /// <response code="200">Выполнено успешно</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="400">Ошибки валидации</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> AddLanguage([FromBody] AddLanguageDto request)
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
        /// DELETE /api/1.0/languages?id=115km5k1-kmkm1515-m51k515m15-51m1msf
        /// </remarks>
        /// <param name="id">id удаляемого языка</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Выполнено успешно</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="400">Ошибки валидации</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Unit>> DeleteLanguage(Guid id)
        {
            var command = new DeleteLanguageCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Обновление языка
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /api/1.0/languages
        /// {
        ///     "Id" : "41k41-511k51jfs-16363mxfs-52n52jkmf",
        ///     "NewName" : "Английский"
        /// }
        /// </remarks>
        /// <param name="request">Id имзеняемого языка, новое название</param>
        /// <returns>Вовзращает пустой ответ</returns>
        /// <response code="204">Выполнено успешно</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="400">Ошибки валидации</response>
        [HttpPut]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Unit>> UpdateLanguage([FromBody] UpdateLanguageDto request)
        {
            var command = _mapper.Map<UpdateLanguageCommand>(request);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
