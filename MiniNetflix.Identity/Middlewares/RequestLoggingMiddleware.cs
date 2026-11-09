namespace MiniNetflix.Identity.Middlewares
{
    /// <summary>
    /// Middleware для логирования входящих HTTP-запросов.
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var startedAt = DateTime.UtcNow;

            try
            {
                await _next(context);
            }
            finally
            {
                var elapsedMs = (DateTime.UtcNow - startedAt).TotalMilliseconds;
                _logger.LogInformation("HTTP {Method} {Path} => {StatusCode} за {ElapsedMs:0} мс",
                    context.Request.Method,
                    context.Request.Path.Value,
                    context.Response.StatusCode,
                    elapsedMs);
            }
        }
    }
}

