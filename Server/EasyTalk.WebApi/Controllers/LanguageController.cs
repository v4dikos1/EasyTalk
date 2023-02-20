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
        [HttpGet]
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
        /// GET /api/v1/languages/aa242kksf-sfkmfsm5345-43k345353-m3m533
        /// </remarks>
        /// <param name="id">id языка</param>
        /// <returns>Возвращает информацию о языке (id, name)</returns>
        /// <response code="200">Выполнено успешно</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="400">Ошибки валидации</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LanguageDetailsVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> AddLanguage([FromBody] AddLanguageDto request)
        {
            var command = _mapper.Map<CreateLanguageCommand>(request);

            var response = await _mediator.Send(command);

            return Ok(response);
        }

        /// <summary>
        /// Удаление языка
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
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
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Unit>> UpdateLanguage(UpdateLanguageDto request)
        {
            var command = _mapper.Map<UpdateLanguageCommand>(request);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
