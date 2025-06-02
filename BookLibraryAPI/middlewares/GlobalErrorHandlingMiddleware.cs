using System.Net;
using System.Text.Json;

namespace BookLibraryAPI.middlewares
{
    public class GlobalErrorHandlingMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;
        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await _next(context);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "خطا.");
                await HandelingErrpor (context, ex);
            }
        }


        public static Task HandelingErrpor(HttpContext context , Exception exception)
        {
            var Code=HttpStatusCode.InternalServerError;
            var Result=string.Empty;

            switch (exception)
            {
                case KeyNotFoundException _:
                    Code=HttpStatusCode.NotFound;
                    Result= JsonSerializer.Serialize(new { error="لم يتم العصور على الموررد!"});
                    break;

                case UnauthorizedAccessException _:
                    Code=HttpStatusCode.Unauthorized;
                    Result = JsonSerializer.Serialize(new { error = "غير مصرح لك " });
                    break;
                case ArgumentException _:
                    Code=HttpStatusCode.BadRequest;
                    Result = JsonSerializer.Serialize(new {error=exception.Message});   
                    break;
                default:
                    Code=HttpStatusCode.InternalServerError;
                    Result = JsonSerializer.Serialize(new {error=exception.Message});
                    break;
            }
            context.Response.ContentType = "application/json";

            context.Response.StatusCode =(int) Code;

            return context.Response.WriteAsync(Result);
        }

        

    }
}
