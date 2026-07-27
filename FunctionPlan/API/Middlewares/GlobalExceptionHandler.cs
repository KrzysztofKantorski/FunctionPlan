using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Middlewares
{
    internal sealed class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {
            if(exception is ValidationException validationException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ValidationProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation error"
                };
                foreach (var error in validationException.Errors)
                {
                    problemDetails.Errors.Add(error.PropertyName, new[] { error.ErrorMessage });
                }

                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
