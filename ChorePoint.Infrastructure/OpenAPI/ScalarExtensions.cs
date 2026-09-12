using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Scalar.AspNetCore;

namespace ChorePoint.Infrastructure.OpenAPI;

public static class ScalarExtensions
{
    public static IServiceCollection AddScalar(this IServiceCollection services)
    {
        services.AddOpenApi(options => options.AddDocumentTransformer<BearerSecuritySchemeTransformer>());

        return services;
    }

    public static WebApplication UseScalar(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return app;
        }

        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options
                .WithTitle(app.Environment.ApplicationName)
                .ForceDarkMode()
                .ExpandAllTags()
                .DisableTelemetry();

            options.Theme = ScalarTheme.Moon;
        });

        return app;
    }
}
