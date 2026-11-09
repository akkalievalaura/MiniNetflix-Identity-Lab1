using System.Net.Mime;
using System.Text.Json;
using MiniNetflix.Identity.DTOs;
using MiniNetflix.Identity.Errors;

namespace MiniNetflix.Identity.Middlewares
{
    /// <summary>
    /// Middleware для централизованной обработки ошибок и формирования корректных HTTP-ответов.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var (statusCode, code, message, logLevel) = ex switch
            {
                ConflictException e => (StatusCodes.Status409Conflict, e.Code, e.Message, LogLevel.Information),
                NotFoundException e => (StatusCodes.Status404NotFound, e.Code, e.Message, LogLevel.Information),
                BusinessException e => (StatusCodes.Status400BadRequest, e.Code, e.Message, LogLevel.Information),
                ExternalServiceException e => (StatusCodes.Status502BadGateway, e.Code, e.Message, LogLevel.Warning),
                InternalAppException e => (StatusCodes.Status500InternalServerError, e.Code, e.Message, LogLevel.Error),
                _ => (StatusCodes.Status500InternalServerError, "internal_error", "Внутренняя ошибка сервера", LogLevel.Error)
            };

            _logger.Log(logLevel, ex, "Ошибка обработки запроса {Method} {Path}. Code={Code}",
                context.Request.Method,
                context.Request.Path,
                code);

            if (context.Response.HasStarted)
                return;

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            var payload = new ErrorResponseDto
            {
                Code = code,
                Message = message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
        }
    }
}

