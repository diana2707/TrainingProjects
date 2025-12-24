using AirportTool.Application.Exceptions;

namespace AirportTool.WebApi.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            //Debugging
            _logger.LogInformation("GlobalExceptionMiddleware invoked");

            try
            {
                await _next(context);
            }
            catch (DomainValidationException ex)
            {
                _logger.LogWarning(ex, "Domain validation failed");
                await HandleExceptionAsync(context, ex, StatusCodes.Status400BadRequest);
            }
            catch (ResourceNotFoundException ex)
            {
                _logger.LogWarning(ex, "Resource was not found");
                await HandleExceptionAsync(context, ex, StatusCodes.Status404NotFound);
            }
            catch (ConflictException ex)
            {
                _logger.LogWarning(ex, "A conflict occurred while processing the request");
                await HandleExceptionAsync(context, ex, StatusCodes.Status409Conflict);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(
                    context,
                    ex,
                    StatusCodes.Status500InternalServerError,
                    overrideMessage: "An unexpected error occurred."
                );
            }
        }

        private Task HandleExceptionAsync(
            HttpContext context,
            Exception ex,
            int statusCode,
            string? overrideMessage = null)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = ex.GetType().Name,
                message = overrideMessage ?? ex.Message
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
