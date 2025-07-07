using App.Errors;
using System.Net;
using System.Text.Json;
using Serilog;

namespace API_Template.Middleware
{
    /// <summary>
    /// Middleware that is in a layer between controller and user response. This is a error middleware that catches error and passes them to the user.
    /// </summary>
    public class ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
    {
        private readonly RequestDelegate _next = next;
        private readonly IHostEnvironment _env = env;
        private readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception using Serilog
                Log.Error(ex, "An unhandled exception occurred while processing the request.");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiException((int)HttpStatusCode.InternalServerError);

                var options = jsonSerializerOptions;

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}