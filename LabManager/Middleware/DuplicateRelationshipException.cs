using LabManager.Exceptions;

namespace LabManager.Middleware
{
    /// <summary>
    /// Catches unhandled exceptions and converts them
    /// into appropriate HTTP responses.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Passes the request to the next middleware.
                await _next(context);
            }
            catch (Exception exception)
            {
                // Records the complete exception in the application logs.
                _logger.LogError(
                    exception,
                    "An exception occurred while processing the request.");

                await HandleExceptionAsync(context, exception);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            int statusCode = exception switch
            {
                ArgumentException =>
                    StatusCodes.Status400BadRequest,

                EntityNotFoundException =>
                    StatusCodes.Status404NotFound,

                DuplicateRelationshipException =>
                    StatusCodes.Status409Conflict,

                _ =>
                    StatusCodes.Status500InternalServerError
            };

            string message =
                statusCode == StatusCodes.Status500InternalServerError
                    ? "An unexpected internal error occurred."
                    : exception.Message;

            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(new
            {
                status = statusCode,
                message
            });
        }
    }
}