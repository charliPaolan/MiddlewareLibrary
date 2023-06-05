using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MiddlewareLibrary.Middleware
{
    public class CustomMiddlewareStep2
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddlewareStep2> _logger;

        public CustomMiddlewareStep2(RequestDelegate next, ILogger<CustomMiddlewareStep2> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Add custom header to the response
            context.Response.Headers.Add("X-Custom-Header-2", "Hello from custom middleware Step 2!");

            // Log a message
            _logger.LogInformation("Custom middleware step 2 is executing.");

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
