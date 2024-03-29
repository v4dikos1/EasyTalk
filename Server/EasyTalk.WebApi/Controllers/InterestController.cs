﻿using AutoMapper;
using EasyTalk.Application.Interests.Commands.CreateInterest;
using EasyTalk.Application.Interests.Commands.DeleteInterest;
using EasyTalk.Application.Interests.Commands.UpdateInterest;
using EasyTalk.Application.Interests.Queries.GetInterestsList;
using EasyTalk.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        /// <returns>Возвращает имя созданного интереса</returns>
        /// <response code="200">Выполнено успешно</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="400">Ошибки валидации</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// Удаление интереса
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE api/1.0/interests/name=книги
        /// </remarks>
        /// <param name="name">id удаляемого интереса</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Выполнено успешно</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="400">Ошибки валидации</response>
        [HttpDelete("{name}")]
        [Authorize]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Delete(string name)
        {
            var command = new DeleteInterestCommand
            {
                Name = name
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
        ///     "Name": "Книги",
        ///     "NewName": "Чтение"
        /// }
        /// </remarks>
        /// <param name="request">Старое имя интереса и новое название</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Выполнено успешно</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="400">Ошибки валидации</response>
        [HttpPut]
        [Authorize]
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
