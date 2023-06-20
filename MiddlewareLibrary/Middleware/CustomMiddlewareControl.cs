using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MiddlewareLibrary.Middleware
{
    public class CustomMiddlewareControl
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddlewareControl> _logger;

        public CustomMiddlewareControl(RequestDelegate next, ILogger<CustomMiddlewareControl> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Add custom header to the response
            context.Response.Headers.Add("X-Custom-Header-Control", "Controlling Configuration");

            // Log a message
            int sec = DateTime.Now.Second;
            _logger.LogInformation($"Custom middleware is executing {sec}.");

            if (sec > -40)
            {
                //context.Response.Headers.Add("Configuration Control", "Control NOT OK - Shutting down");

                var lifetime = context.RequestServices.GetRequiredService<IHostApplicationLifetime>();
                lifetime.StopApplication();
                return;
            }
            else if (sec > 20)
            {
                //context.Response.Headers.Add("Configuration Control", "Control NOT OK - Returning");

                return;
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
