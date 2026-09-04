using System.Text;

using ChorePoint.Application.Interfaces;
using ChorePoint.Application.Interfaces.Hangfire;
using ChorePoint.Infrastructure.Authentication;
using ChorePoint.Infrastructure.Hangfire.Jobs;

using Hangfire;
using Hangfire.Redis.StackExchange;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using StackExchange.Redis;

namespace ChorePoint.Infrastructure;

public static class DependencyInjection
{
    private static ConnectionMultiplexer? RedisConnection;

    public static IServiceCollection AddInfrastructureOptions(this IServiceCollection services, string? redisConnectionString = null)
    {
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IKidLoginCodeGenerator, KidLoginCodeGenerator>();
        services.AddScoped<IParentContextService, ParentContextService>();
        services.AddScoped<IPasswordHasher<string>, PasswordHasher<string>>();

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!))
            });

        if (redisConnectionString is not null)
        {
            RedisConnection = ConnectionMultiplexer.Connect(redisConnectionString);

            services.AddHangfire(configuration => configuration.UseRedisStorage(RedisConnection));
            services.AddHangfireServer();

            services.AddTransient<ILoginCodeDeletionJob, LoginCodeDeletionJob>();
        }

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddHangfire(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddCaching(this IServiceCollection services)
    {
        return services;
    }

    public static IHostApplicationBuilder AddDatabase(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<AppDbContext>("database-connection", configureDbContextOptions: options => options.EnableSensitiveDataLogging());
        builder.Services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

        return builder;
    }
}
