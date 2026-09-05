using System.Text;

using ChorePoint.Application.Interfaces;
using ChorePoint.Infrastructure.Authentication;
using ChorePoint.Infrastructure.Options;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace ChorePoint.Infrastructure.ServiceExtensions;

public static class AuthenticationExtensions
{
    public static IHostApplicationBuilder AddAuthentication(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IKidLoginCodeGenerator, KidLoginCodeGenerator>();
        services.AddScoped<IParentContextService, ParentContextService>();
        services.AddScoped<IPasswordHasher<string>, PasswordHasher<string>>();

        var authOptions = builder.Configuration.GetSection(AuthenticationOptions.ConfigurationSectionName).Get<AuthenticationOptions>()
                          ?? throw new InvalidOperationException("Authentication options could not be populated correctly");

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = authOptions.JwtIssuer,
                ValidAudience = authOptions.JwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.JwtKey))
            });

        services.Configure<AuthenticationOptions>(builder.Configuration.GetSection(AuthenticationOptions.ConfigurationSectionName));

        return builder;
    }
}
