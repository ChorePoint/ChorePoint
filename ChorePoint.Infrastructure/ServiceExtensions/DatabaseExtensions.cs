using ChorePoint.Application.Interfaces;
using ChorePoint.Infrastructure.Options;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChorePoint.Infrastructure.ServiceExtensions;

public static class DatabaseExtensions
{
    public static IHostApplicationBuilder AddDatabase(this IHostApplicationBuilder builder)
    {
        var databaseOptions = builder.Configuration.GetSection(DatabaseOptions.ConfigurationSectionName).Get<DatabaseOptions>()
                          ?? throw new InvalidOperationException("Database options could not be populated correctly");

        Action<DbContextOptionsBuilder>? optionsBuilder = null;
        if (databaseOptions.EnableSensitiveLogging)
        {
            optionsBuilder = options => options.EnableSensitiveDataLogging();
        }
        builder.AddNpgsqlDbContext<AppDbContext>("database-connection", configureDbContextOptions: optionsBuilder);

        var services = builder.Services;

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

        services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.ConfigurationSectionName));

        return builder;
    }
}
