using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MiddlewareLibrary.Middleware
{
    public class CustomMiddlewareStep3
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddlewareStep3> _logger;

        public CustomMiddlewareStep3(RequestDelegate next, ILogger<CustomMiddlewareStep3> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Add custom header to the response
            context.Response.Headers.Add("X-Custom-Header-3", "this is the last custom middleware!");

            // Log a message
            _logger.LogInformation("Custom middleware step 3 is executing.");

            var req = context.Request;

            // Access the value of the custom header set in CustomMiddleware1
            if (context.Response.Headers.ContainsKey("EndpointsStatus"))
            {
                string endpointsStatus = context.Response.Headers["EndpointsStatus"];
                _logger.LogInformation($"EndpointsStatus: {endpointsStatus}");
            }
            if (context.Response.Headers.ContainsKey("FunctionlitiesStatus"))
            {
                string functionlitiesStatus = context.Response.Headers["FunctionlitiesStatus"];
                _logger.LogInformation($"FunctionlitiesStatus: {functionlitiesStatus}");
            }

            _logger.LogInformation("Custom middleware step 3 has been executed.");

            // Call the next middleware in the pipeline
            await _next(context);

            _logger.LogInformation("Custom middleware step 3 on the way back.");
        }
    }
}
