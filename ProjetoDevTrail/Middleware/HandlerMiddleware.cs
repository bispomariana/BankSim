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

            object payload = response;

            // 2. Se for Desenvolvimento, criamos um objeto novo contendo o InnerError
            if (_env.IsDevelopment())
            {
                response.Details = exception.StackTrace;

                // Em erro 500, pegamos a mensagem real da exceção
                if (response.StatusCode == 500)
                    response.Message = exception.Message;

                // AQUI ESTÁ O PULO DO GATO:
                // Criamos um objeto anônimo que tem tudo que o response tem + o InnerError
                payload = new
                {
                    response.StatusCode,
                    response.Message,
                    response.Details,
                    // Pega o erro interno (onde está o detalhe do banco de dados)
                    InnerError = exception.InnerException?.Message
                };
            }

            context.Response.StatusCode = response.StatusCode;

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            // 3. Serializamos o 'payload' (que pode ter o InnerError) em vez do 'response'
            var json = JsonSerializer.Serialize(payload, jsonOptions);

            await context.Response.WriteAsync(json);
        }
    }
}
