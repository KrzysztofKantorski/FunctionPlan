using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace API.Extensions
{
    public static class GlobalRateLimiterExtensions
    {
        public static IServiceCollection AddGlobalRateLimiting(this IServiceCollection services) 
        {
            services.AddRateLimiter(options =>
            {

                options.AddFixedWindowLimiter("GlobalLimit", opt =>
                {
                    //Users can make up to 50 requests a minute
                    opt.PermitLimit = 50;
                    opt.Window = TimeSpan.FromMinutes(1);
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    opt.QueueLimit = 0;
                });

                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                //Add error message
                options.OnRejected = async (context, cancellationToken) =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    var problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status429TooManyRequests,
                        Title = "Too Many Requests",
                        Detail = "Request limit has been exceeded."
                    };

                    await context.HttpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
                };
            });

            return services;
        }
    }
}
