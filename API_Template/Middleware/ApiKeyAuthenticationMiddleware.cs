namespace API_Template.Middleware
{
    public class ApiKeyAuthenticationMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;
        private const string API_KEY = "Secret_Key"; // Replace with your actual API key

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey) ||
                extractedApiKey != API_KEY)
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            await _next(context);
        }
    }
}
