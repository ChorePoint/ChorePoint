using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

using Scalar.AspNetCore;

namespace ChorePoint.Infrastructure.OpenAPI;

public static class ScalarExtensions
{
    public static WebApplication AddScalar(this WebApplication app)
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
