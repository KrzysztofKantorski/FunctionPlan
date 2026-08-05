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

                validationProblemDetails.Errors = validationException.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorMessage).ToArray()
                    );

                await httpContext.Response.WriteAsJsonAsync(validationProblemDetails, cancellationToken);
                return true;
            }



            //Application layer errors (handlers)

            if (exception is AppException appException) 
            { 
                httpContext.Response.StatusCode = appException.StatusCode;

                var response = new 
                { 
                    Status = appException.StatusCode, 
                    Error = appException.Message 
                };

                await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
                return true;
            }



            //Domain layer exceptions

            if (exception is Domain.Common.DomainException domainException) 
            { 
                httpContext.Response.StatusCode = domainException.StatusCode;
                var customDetails = new ProblemDetails
                {
                    Status = domainException.StatusCode,
                    Title = domainException.GetType().Name, 
                    Detail = domainException.Message
                };

                await httpContext.Response.WriteAsJsonAsync(customDetails, cancellationToken);
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
