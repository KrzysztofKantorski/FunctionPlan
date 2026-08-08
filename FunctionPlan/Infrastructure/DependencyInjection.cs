using Application.Abstractions.Cache;
using Application.Abstractions.Data;
using Application.Abstractions.Email;
using Application.Abstractions.Mail;
using Application.Abstractions.Security;
using Application.Abstractions.Security.Tokens;
using Domain.Common;
using Domain.Meetings;
using Domain.RefreshTokens;
using Domain.Users;
using Infrastructure.Cache;
using Infrastructure.Email;
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
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();


            //SQL
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));


            //Redis
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["REDIS_CONN_STRING"] ?? string.Empty;
            });


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

            //Email
            services.Configure<EmailSettings>(options =>
            {
                options.SmtpHost = configuration["SMTP_HOST"] ?? string.Empty;
                if (int.TryParse(configuration["SMTP_PORT"], out int port))
                {
                    options.SmtpPort = port;
                }
                options.SmtpPassword = configuration["SMTP_PASSWORD"] ?? string.Empty;
                options.SenderUsername = configuration["SENDER_USERNAME"] ?? string.Empty;
                options.SenderEmail = configuration["SENDER_EMAIL"] ?? string.Empty;
                options.SmtpUsername = configuration["SMTP_USERNAME"] ?? string.Empty;
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
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<IOTPGenerator, OTPGenerator>();

            return services;
        }
    }
}
