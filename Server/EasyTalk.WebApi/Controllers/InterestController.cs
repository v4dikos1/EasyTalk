using AutoMapper;
using EasyTalk.Application.Interests.Commands.CreateInterest;
using EasyTalk.Application.Interests.Commands.DeleteInterest;
using EasyTalk.Application.Interests.Commands.UpdateInterest;
using EasyTalk.Application.Interests.Queries.GetInterestDetails;
using EasyTalk.Application.Interests.Queries.GetInterestsList;
using EasyTalk.Application.Pictures.Commands.AddPicture;
using EasyTalk.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EasyTalk.WebApi.Controllers
{
    [ApiController]
    [ApiVersionNeutral]
    [Route("api/{version:apiVersion}/interests")]
    [Produces("application/json")]
    public class InterestController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public InterestController(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        /// <summary>
        /// Создание интереса
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST api/1.0/interests
        /// {
        ///     "Name" : "Фильмы"
        /// }
        /// </remarks>
        /// <param name="request">Название интереса</param>
        /// <returns>Возвращает id (guid) созданного интереса</returns>
        /// <response code="200">Выполнено успешно</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="400">Ошибки валидации</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateInterest([FromBody] CreateInterestDto request)
        {
            var command = _mapper.Map<CreateInterestCommand>(request);

            var response = await _mediator.Send(command);

            return Ok(response);
        }

        /// <summary>
        /// Получение списка интересов
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET api/1.0/interests/0/10
        /// </remarks>
        /// <param name="offset">Смещение</param>
        /// <param name="limit">Ограничение</param>
        /// <returns>Возвращает списко интересов с заданными смещением и ограничением</returns>
        /// <response code="200">Выполнено успешно</response>
        /// <response code="400">Ошибки валидации</response>
        [HttpGet("{offset}/{limit}")]
        [ProducesResponseType(typeof(InterestsListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InterestsListViewModel>> GetAll(int offset, int limit)
        {
            var query = new GetInterestsListQuery
            {
                Limit = limit,
                Offset = offset
            };

            var response = await _mediator.Send(query);

            return response;
        }

        /// <summary>
        /// Получение интереса
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET api/1.0/interests?id=sks8u8sj-am8sb2nz-msuh7v71-snj7vgnw
        /// </remarks>
        /// <param name="id">id интереса</param>
        /// <returns>Возвращает интерес по заданному id</returns>
        /// <response code="200">Выполнено успешно</response>
        /// <response code="400">Ошибки валидации</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InterestLookupDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InterestLookupDto>> Get(Guid id)
        {
            var query = new GetInterestDetailQuery
            {
                Id = id
            };

            var response = await _mediator.Send(query);

            return response;
        }

        /// <summary>
        /// Удаление интереса
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE api/1.0/interests?id=sks8u8sj-am8sb2nz-msuh7v71-snj7vgnw
        /// </remarks>
        /// <param name="id">id удаляемого интереса</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Выполнено успешно</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="400">Ошибки валидации</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteInterestCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Обновление интереса
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT api/1.0/interests
        /// {
        ///     "Id": "sks8u8sj-am8sb2nz-msuh7v71-snj7vgnw",
        ///     "NewName": "Чтение"
        /// }
        /// </remarks>
        /// <param name="request">Id интереса и новое название</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Выполнено успешно</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="400">Ошибки валидации</response>
        [HttpPut]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Update([FromBody] UpdateInterestDto request)
        {
            var command = _mapper.Map<UpdateInterestCommand>(request);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
