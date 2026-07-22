using ChorePoint.API.Documentation;
using ChorePoint.API.Middleware;
using ChorePoint.Application.Behaviours;
using ChorePoint.Application.Handlers.Auth.Login;
using ChorePoint.Infrastructure;
using ChorePoint.ServiceDefaults;
using FluentValidation;
using MediatR;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using ZiggyCreatures.Caching.Fusion;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateBootstrapLogger();

Log.Information("Program.cs starting host ≧◡≦");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddServiceDefaults();

    builder.AddNpgsqlDbContext<AppDbContext>("chorepoint-db-cs");

    builder.Services.AddControllers();
    builder.Services.AddOpenApi(options =>
    {
        options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
    });

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    builder.Services.AddMemoryCache();
    var cacheBuilder = builder
        .Services.AddFusionCache()
        .WithDefaultEntryOptions(
            new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromMinutes(5),

                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromHours(1),
                FailSafeThrottleDuration = TimeSpan.FromSeconds(30),

                EagerRefreshThreshold = 0.9f,

                FactorySoftTimeout = TimeSpan.FromSeconds(100),
            }
        );
    if (builder.Environment.IsDevelopment())
        cacheBuilder.WithNullImplementation();

    builder.Services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(typeof(LoginHandler).Assembly)
    );
    builder.Services.AddValidatorsFromAssembly(typeof(LoginValidator).Assembly);

    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

    builder.Services.AddInfrastructure();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            "AllowAngular",
            policy =>
            {
                policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
            }
        );
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options
                .WithTitle("ChorePoint API")
                .WithClassicLayout()
                .ForceDarkMode()
                .ExpandAllTags()
                .HideSearch()
                .HideModels();

            options.Theme = ScalarTheme.Solarized;
        });
    }

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.UseExceptionHandler();

    app.UseCors("AllowAngular");

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.MapDefaultEndpoints();

    await app.RunAsync();
}
// See https://github.com/dotnet/efcore/issues/29923
catch (Exception ex)
    when (ex is not HostAbortedException && ex.Source is not "Microsoft.EntityFrameworkCore.Design")
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Bye bye... (ㄒoㄒ)");
    await Log.CloseAndFlushAsync();
}
