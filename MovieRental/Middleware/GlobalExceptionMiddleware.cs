using System.Net;
using System.Text.Json;

namespace MovieRental.Middleware
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

        public async Task InvokeAsync(HttpContext context)
        {
            //run the operation at that time
            //if any exception is catched
            //use HandleExceptionAsync to mapp and throw specific error
            //this allows us to remove try/catch in the controllers/features and go by the SOLID principals
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode = exception switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                // this allows us to build specific ones also
                CustomerNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = statusCode;

            var problem = new
            {
                Status = statusCode,
                Title = statusCode switch
                {
                    400 => "Bad Request",
                    404 => "Not Found",
                    _ => "Internal Server Error"
                },
                Detail = exception.Message
            };

            var json = JsonSerializer.Serialize(problem);

            return context.Response.WriteAsync(json);
        }
    }

    #region specific errors
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException()
            : base("No customers found!") { }
    }

    public class RentalNotFoundException: Exception
    {
        public RentalNotFoundException(string name) 
        : base($"Rental '{name}' not found.") { }
    }

    #endregion
}