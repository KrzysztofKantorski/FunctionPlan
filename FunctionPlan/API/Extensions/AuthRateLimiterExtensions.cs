using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace API.Extensions
{
    public static class AuthRateLimiterExtensions
    {
        public static IServiceCollection AddAuthRateLimiter(this IServiceCollection services) 
        {
            services.AddRateLimiter(options =>
            {

                //Login limit
                options.AddFixedWindowLimiter("LoginLimit", opt =>
                {
                    opt.PermitLimit = 5;
                    opt.Window = TimeSpan.FromMinutes(10);
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    opt.QueueLimit = 0;
                });


                //Register and OTP limit
                options.AddFixedWindowLimiter("OtpRegisterLimit", opt =>
                {
                    opt.PermitLimit = 3;
                    opt.Window = TimeSpan.FromMinutes(15);
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    opt.QueueLimit = 0;
                });


                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

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
