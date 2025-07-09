using Microsoft.Extensions.Configuration;

namespace API_Template.Middleware
{
    public class ApiKeyAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        private readonly RequestDelegate _next = next;

        //TODO: Setup ApiKey in appsettings.Development.json
        private readonly string _apiKey = configuration["ApiKey"] ?? string.Empty;

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey) ||
                extractedApiKey != _apiKey)
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            await _next(context);
        }
    }
}