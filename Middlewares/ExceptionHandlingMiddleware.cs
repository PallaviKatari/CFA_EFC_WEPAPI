using System.Net;
using System.Text.Json;

namespace CFA_EFC_WEPAPI.Middlewares
{
    // Custom Middleware for Global Exception Handling
    // This middleware captures exceptions thrown during request processing
    // 3 steps:
    // 1. RequestDelegate to call the next middleware in the pipeline
    // 2. ILogger to log exception details
    // 3. InvokeAsync method to handle the request and catch exceptions
    // 4. HttpContext to access request and response details
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        // Default Logger interface in .NET for logging
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        // Constructor to initialize the middleware with the next delegate and logger
        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        //InvokeAsync method to process the HTTP request in the middleware pipeline
        //Parameter: HttpContext - encapsulates all HTTP-specific information about an individual HTTP request
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // await the next middleware in the pipeline
                // _next represents the next middleware component
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception details
                // LogError method logs error messages
                // LogInformation includes exception object and message
                // LogWarning is for warnings
                // LogDebug is for debugging information
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        // Whatever Http Request comes in, this method will handle the exception
        // If there is any exception in the request processing, this method will create a standardized error response
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = context.Response;

            var errorResponse = new ApiErrorResponse();

            switch (exception)
            {
                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.StatusCode = response.StatusCode;
                    errorResponse.Message = "Resource not found.";
                    break;

                case UnauthorizedAccessException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponse.StatusCode = response.StatusCode;
                    errorResponse.Message = "Unauthorized access.";
                    break;

                case ArgumentException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.StatusCode = response.StatusCode;
                    errorResponse.Message = exception.Message;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.StatusCode = response.StatusCode;
                    errorResponse.Message = "An unexpected error occurred.";
                    break;
            }

            //JsonSerializer to convert the errorResponse object to a JSON string
            //Present in System.Text.Json namespace
            var result = JsonSerializer.Serialize(errorResponse);
            return context.Response.WriteAsync(result);
        }
    }

    // Standardized structure for API error responses
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
