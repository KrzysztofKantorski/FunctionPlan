using Microsoft.AspNetCore.Mvc;
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
                options.AddPolicy("LoginLimit", options =>
                {
                    //Get user ip
                    var clientIp = options.Connection.RemoteIpAddress?.ToString() ?? "unknown-ip";

                    return RateLimitPartition.GetFixedWindowLimiter(partitionKey: clientIp, factory: _ =>
                        new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 5,
                            Window = TimeSpan.FromMinutes(10),
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            QueueLimit = 0
                        });

                });


                //Register and OTP limit
                options.AddPolicy("OtpRegisterLimit", options =>
                {
                    var clientIp = options.Connection.RemoteIpAddress?.ToString() ?? "unknown-ip";

                    return RateLimitPartition.GetFixedWindowLimiter(partitionKey: clientIp, factory: _ =>
                        new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 3,
                            Window = TimeSpan.FromMinutes(15),
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            QueueLimit = 0
                        });

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
