using Microsoft.Extensions.DependencyInjection;

namespace ChorePoint.Infrastructure.Middleware.ExceptionHandling;

public static class ExceptionHandlerExtensions
{
    public static IServiceCollection AddExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}
