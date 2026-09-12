using ChorePoint.Application;
using ChorePoint.Infrastructure.BuilderExtensions;
using ChorePoint.Infrastructure.Middleware;
using ChorePoint.Infrastructure.Middleware.ExceptionHandling;
using ChorePoint.Infrastructure.OpenAPI;
using ChorePoint.ServiceDefaults;

using Hangfire;

using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

Log.Logger = new LoggerConfiguration().WriteTo.Console(theme: AnsiConsoleTheme.Code).CreateBootstrapLogger();

Log.Information("Starting ChorePoint.API.csproj ≧◡≦");

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
    services.AddHttpContextAccessor();
    services.AddScalar();

    services.AddExceptionHandler();
    services.AddGlobalRateLimiter();

    var app = builder.Build();

    app.MapControllers();
    app.MapDefaultEndpoints();
    app.UseScalar();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseExceptionHandler();
    app.UseRateLimiter();
    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseHangfireDashboard();

    await app.RunAsync();
}
// See https://github.com/dotnet/efcore/issues/29923
catch (Exception ex) when (ex is not HostAbortedException && ex.Source is not "Microsoft.EntityFrameworkCore.Design")
{
    Log.Fatal(ex, "ChorePoint.API.csproj startup terminated unexpectedly! (╯'□')╯︵ ┻━┻");
}
finally
{
    Log.Information("API out! ᕦ(ò_óˇ)ᕤ");
    await Log.CloseAndFlushAsync();
}
