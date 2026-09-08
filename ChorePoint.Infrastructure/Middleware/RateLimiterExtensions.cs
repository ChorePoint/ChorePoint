using System.Threading.RateLimiting;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ChorePoint.Infrastructure.Middleware;

public static class RateLimiterExtensions
{
    public static IServiceCollection AddGlobalRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options => options.GlobalLimiter =
            PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            {
                string path = httpContext.Request.Path.ToString();

                if (path.StartsWith("/api/auth/login/kid"))
                {
                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: $"{httpContext.Connection.RemoteIpAddress}-kid-login",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 5,
                            Window = TimeSpan.FromSeconds(30)
                        });
                }

                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 250,
                        Window = TimeSpan.FromMinutes(1)
                    });
            }));

        return services;
    }
}
