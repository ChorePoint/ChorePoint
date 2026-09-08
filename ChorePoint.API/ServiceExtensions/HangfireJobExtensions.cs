using ChorePoint.Application.Interfaces.Hangfire;
using ChorePoint.Infrastructure.Hangfire;

namespace ChorePoint.API.ServiceExtensions;

public static class HangfireJobExtensions
{
    public static IServiceCollection AddHangfireJobs(this IServiceCollection services)
    {
        services.AddTransient<ILoginCodeDeletionJob, LoginCodeDeletionJob>();

        return services;
    }
}
