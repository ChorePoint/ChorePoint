using System.Net;
using System.Security.Claims;

using ChorePoint.Domain.Exceptions;

using FluentValidation;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ChorePoint.API.Middleware;

public partial class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ProblemDetails problemDetails = new() { Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}" };

        (problemDetails.Status, problemDetails.Title, problemDetails.Detail) = exception switch
        {
            NotFoundException notFoundEx => ((int)HttpStatusCode.NotFound, "Not Found", notFoundEx.Message),

            ValidationException => (
                (int)HttpStatusCode.BadRequest,
                "Validation Error",
                "One or more validation errors occurred."
            ),

            UnauthorizedAccessException unauthorizedEx => (
                (int)HttpStatusCode.Unauthorized,
                "Unauthorized",
                unauthorizedEx.Message
            ),

            DomainException domainEx => ((int)HttpStatusCode.BadRequest, "Bad Request", domainEx.Message),

            _ => (
                (int)HttpStatusCode.InternalServerError,
                "An internal server error has occurred.",
                "Please try again later."
            )
        };

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions["errors"] = validationException.Errors.Select(e => new
            {
                property = e.PropertyName,
                error = e.ErrorMessage
            });
        }

        if (problemDetails.Status is (int)HttpStatusCode.InternalServerError)
        {
            LogUnexpectedException(
                httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                httpContext.User.FindFirst(ClaimTypes.Role)?.Value,
                httpContext.Request.Path.Value, exception
            );
        }
        else
        {
            LogUnsuccessfulRequest(
                httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                httpContext.User.FindFirst(ClaimTypes.Role)?.Value,
                httpContext.Request.Path.Value, exception
            );
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    [LoggerMessage(LogLevel.Error, "An unexpected exception occurred during a request from parent ID [{ParentId}] using role [{Role}] on resource path [{ResourcePath}]")]
    partial void LogUnexpectedException(string? parentId, string? role, string? resourcePath, Exception exception);

    [LoggerMessage(LogLevel.Information, "An unsuccessful response occurred during a request from parent ID [{ParentId}] using role [{Role}] on resource path [{ResourcePath}]")]
    partial void LogUnsuccessfulRequest(string? parentId, string? role, string? resourcePath, Exception exception);
}
