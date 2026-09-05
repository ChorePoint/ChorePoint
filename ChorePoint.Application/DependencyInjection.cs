using ChorePoint.Application.Behaviours;
using ChorePoint.Application.Policies.Shop;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChorePoint.Application;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddApplication(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddScoped<IShopOpenPolicy, ShopOpenPolicy>();

        var applicationAssembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        services.AddValidatorsFromAssembly(applicationAssembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.Configure<ApiOptions>(builder.Configuration.GetSection(ApiOptions.ConfigurationSectionName));

        return builder;
    }
}
