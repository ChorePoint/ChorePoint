using ChorePoint.Application.Behaviours;
using ChorePoint.Application.HangfireJobs;
using ChorePoint.Application.Policies;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChorePoint.Application;

public static class ApplicationExtensions
{
    public static IHostApplicationBuilder AddApplication(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddTransient<ILoginCodeDeletionJob, LoginCodeDeletionJob>();

        services.AddScoped<IShopOperationPolicy, ShopOperationPolicy>();

        var applicationAssembly = typeof(ApplicationExtensions).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        services.AddValidatorsFromAssembly(applicationAssembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.Configure<ApiOptions>(builder.Configuration.GetSection(ApiOptions.ConfigurationSectionName));

        return builder;
    }
}
