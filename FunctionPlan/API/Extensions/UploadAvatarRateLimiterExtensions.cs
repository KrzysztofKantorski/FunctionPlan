using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.RateLimiting;

namespace API.Extensions
{
    public static class UploadAvatarRateLimiterExtensions
    {
        public static IServiceCollection AddAvatarUploaderRateLimiterExtensions(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.AddPolicy("AvatarUploader", context =>
                {
                    
                    var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                 ?? context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                 ?? "unauthorized";


                    // Add rate limiter for each user id
                    return RateLimitPartition.GetFixedWindowLimiter(partitionKey: userId, factory: _ =>
                        new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 3,
                            Window = TimeSpan.FromMinutes(60),
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
                        Title = "Image changed too often",
                        Detail = "Request limit has been exceeded."
                    };

                    await context.HttpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
                };
            });
            return services;
        }
    }
}
