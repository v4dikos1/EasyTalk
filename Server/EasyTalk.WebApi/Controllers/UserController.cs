﻿using AutoMapper;
using EasyTalk.Application.Users.Commands.Registration;
using EasyTalk.WebApi.Models.User;
using MediatR;
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
        /// <param name="file">Аватар</param>
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
    }
}
