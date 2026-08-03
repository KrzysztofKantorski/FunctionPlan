using Application.Abstractions.Data;
using Application.Abstractions.Security;
using Domain.Common;
using Domain.Meetings;
using Domain.Users;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Data;
using Infrastructure.Persistence.Repository;
using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            //Database
            var connectionString = configuration["CONN_STRING"];

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string not found");
            }

            //Register DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));


            //Repositories
            services.AddScoped<IMeetingRepository, MeetingRepository>();
            services.AddScoped<IUserRepository, AuthRepository>();


            //SQL
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));


            //JWT
            services.Configure<JwtSettings>(options =>
            {
                options.Secret = configuration["JWT_SECRET"] ?? string.Empty;
                options.Issuer = configuration["JWT_ISSUER"] ?? string.Empty;
                options.Audience = configuration["JWT_AUDIENCE"] ?? string.Empty;

                if (int.TryParse(configuration["JWT_EXPIRY_MINUTES"], out int expiry))
                {
                    options.ExpiryMinutes = expiry;
                }

            });


            //Refresh token
            services.Configure<RefreshTokenSettings>(options =>
            {
                if (int.TryParse(configuration["REFRESH_TOKEN_EXPIRY_DAYS"], out int expiry))
                {
                    options.ExpiryDays = expiry;
                }
            });


            //Register services
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IJwtProvider, JwtTokenGenerator>();
            services.AddSingleton<IRefreshTokenGenerator, RefreshTokenGenerator>();


            return services;
        }
    }
}
