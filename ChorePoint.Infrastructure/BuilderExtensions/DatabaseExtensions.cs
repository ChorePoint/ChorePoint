using ChorePoint.Application.Interfaces;
using ChorePoint.Infrastructure.Options;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChorePoint.Infrastructure.BuilderExtensions;

public static class DatabaseExtensions
{
    public static IHostApplicationBuilder AddDatabase(this IHostApplicationBuilder builder)
    {
        var databaseOptions = builder.Configuration.GetSection(DatabaseOptions.ConfigurationSectionName).Get<DatabaseOptions>()
                          ?? throw new InvalidOperationException("Database options could not be populated correctly");

        // No DbContext pooling due to issue with scopes when using IMediator from AppDbContext.cs
        Action<DbContextOptionsBuilder> optionsBuilder = options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("database-connection"));
        if (databaseOptions.EnableSensitiveLogging)
        {
            optionsBuilder += options => options.EnableSensitiveDataLogging();
        }
        builder.Services.AddDbContext<AppDbContext>(optionsBuilder);
        builder.EnrichNpgsqlDbContext<AppDbContext>();

        var services = builder.Services;

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

        services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.ConfigurationSectionName));

        return builder;
    }
}
