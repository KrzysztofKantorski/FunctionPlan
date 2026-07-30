using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace API.Extensions
{
    public static class RateLimiterExtensions
    {
        public static IServiceCollection AddRateLimiting(this IServiceCollection services) 
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
            });

            return services;
        }
    }
}
