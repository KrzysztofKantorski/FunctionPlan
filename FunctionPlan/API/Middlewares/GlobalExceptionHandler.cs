using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Middlewares
{
    internal sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {

            //Log every unhandled error

            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);


            //Fluent validation errors

            if (exception is ValidationException validationException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                var validationProblemDetails = new ValidationProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation error"
                };

                foreach (var error in validationException.Errors)
                {
                    validationProblemDetails.Errors.Add(error.PropertyName, new[] { error.ErrorMessage });
                }

                await httpContext.Response.WriteAsJsonAsync(validationProblemDetails, cancellationToken);
                return true;
            }


            //Server errors
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error",
                Detail = "An unexpected error occurred while processing your request."
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
