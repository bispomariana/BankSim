using System.Net;
using System.Text.Json;

namespace ProjetoDevTrail.Api.Middleware
{
    public class HandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HandlerMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public HandlerMiddleware(RequestDelegate next,ILogger<HandlerMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse();

            switch (exception)
            {
                case ArgumentException:
                case ApplicationException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = exception.Message;
                    break;

                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "The requested resource was not found.";
                    break;

                case UnauthorizedAccessException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Message = "Access denied.";
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = "An error occurred on the server. Please try again later.";
                    break;
            }
             

            context.Response.StatusCode = response.StatusCode;

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, jsonOptions);

            await context.Response.WriteAsync(json);
        }
    }
}
