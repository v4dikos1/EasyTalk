using EasyTalk.Application.Common.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace EasyTalk.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.Errors);
                    break;

                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;

                case AlreadyExistsException:
                    code = HttpStatusCode.Conflict;
                    break;

                case InvalidLoginOrPasswordException:
                    code = HttpStatusCode.Unauthorized;
                    break;

                case UserOperationCancelledException:
                    code = HttpStatusCode.Forbidden;
                    break;

                case DialogOperationCancelledException:
                    code = HttpStatusCode.Forbidden;
                    break;

            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
            {
                Console.WriteLine(exception);
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }
}
