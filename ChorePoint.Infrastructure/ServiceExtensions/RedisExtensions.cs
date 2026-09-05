using ChorePoint.Infrastructure.Options;

using Hangfire;
using Hangfire.Redis.StackExchange;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using StackExchange.Redis;

using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Infrastructure.ServiceExtensions;

public static class RedisExtensions
{
    private static ConnectionMultiplexer? s_redisConnection;

    extension(IHostApplicationBuilder builder)
    {
        public IHostApplicationBuilder AddHangfire()
        {
            var redisConnectionString = builder.Configuration.GetConnectionString("redis")
                                        ?? throw new InvalidOperationException("Redis connection string does not exist");
            s_redisConnection = ConnectionMultiplexer.Connect(redisConnectionString);

            var services = builder.Services;

            services.AddHangfire(configuration => configuration.UseRedisStorage(s_redisConnection));
            services.AddHangfireServer();

            return builder;
        }

        public IHostApplicationBuilder AddCaching()
        {
            var services = builder.Services;

            services.AddMemoryCache();
            var cacheBuilder = services
                .AddFusionCache()
                .WithDefaultEntryOptions(
                    new FusionCacheEntryOptions
                    {
                        Duration = TimeSpan.FromMinutes(5),

                        IsFailSafeEnabled = true,
                        FailSafeMaxDuration = TimeSpan.FromHours(1),
                        FailSafeThrottleDuration = TimeSpan.FromSeconds(30),

                        EagerRefreshThreshold = 0.9f,

                        FactorySoftTimeout = TimeSpan.FromSeconds(100)
                    }
                );

            var cacheOptions = builder.Configuration.GetSection(CacheOptions.ConfigurationSectionName).Get<CacheOptions>()
                               ?? throw new InvalidOperationException("Cache options could not be populated correctly");
            if (!cacheOptions.EnableCaching)
            {
                cacheBuilder.WithNullImplementation();
            }

            return builder;
        }
    }
}
