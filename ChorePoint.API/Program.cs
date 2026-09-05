using ChorePoint.API.Documentation;
using ChorePoint.API.Middleware;
using ChorePoint.Application;
using ChorePoint.Application.Interfaces.Hangfire;
using ChorePoint.Infrastructure.Hangfire.Jobs;
using ChorePoint.Infrastructure.ServiceExtensions;
using ChorePoint.ServiceDefaults;

using Hangfire;

using Scalar.AspNetCore;

using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

Log.Logger = new LoggerConfiguration().WriteTo.Console(theme: AnsiConsoleTheme.Code).CreateBootstrapLogger();

Log.Information("Program.cs starting API ≧◡≦");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddServiceDefaults();

    builder.AddAuthentication();
    builder.AddDatabase();
    builder.AddCaching();
    builder.AddHangfire();
    builder.AddApplication();

    var services = builder.Services;

    services.AddControllers();
    services.AddOpenApi(options => options.AddDocumentTransformer<BearerSecuritySchemeTransformer>());

    services.AddHttpContextAccessor();

    services.AddExceptionHandler<GlobalExceptionHandler>();
    services.AddProblemDetails();

    services.AddTransient<ILoginCodeDeletionJob, LoginCodeDeletionJob>();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options
                .WithTitle("ChorePoint API")
                .ForceDarkMode()
                .ExpandAllTags()
                .DisableTelemetry();

            options.Theme = ScalarTheme.Moon;
        });
    }

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.UseExceptionHandler();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.MapDefaultEndpoints();

    app.UseHangfireDashboard();

    await app.RunAsync();
}
// See https://github.com/dotnet/efcore/issues/29923
catch (Exception ex) when (ex is not HostAbortedException && ex.Source is not "Microsoft.EntityFrameworkCore.Design")
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Bye bye... (ㄒoㄒ)");
    await Log.CloseAndFlushAsync();
}
