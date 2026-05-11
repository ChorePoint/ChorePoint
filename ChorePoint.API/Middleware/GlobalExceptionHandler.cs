using System.Net;
using ChorePoint.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ChorePoint.API.Middleware;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError("A global exception occurred: {Message}", exception.Message);

        var problemDetails = new ProblemDetails
        {
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
        };

        (problemDetails.Status, problemDetails.Title, problemDetails.Detail) = exception switch
        {
            NotFoundException notFoundEx =>
                ((int)HttpStatusCode.NotFound, "Not Found", notFoundEx.Message),

            ValidationException =>
                ((int)HttpStatusCode.BadRequest, "Validation Error", "One or more validation errors occurred."),

            UnauthorizedAccessException unauthorizedEx =>
                ((int)HttpStatusCode.Unauthorized, "Unauthorized", unauthorizedEx.Message),

            DomainException domainEx =>
                ((int)HttpStatusCode.BadRequest, "Bad Request", domainEx.Message),

            _ =>
                ((int)HttpStatusCode.InternalServerError, "An internal server error has occurred.",
                    "Please try again later.")
        };

        if (exception is ValidationException validationException)
            problemDetails.Extensions["errors"] = validationException.Errors
                .Select(e => new { property = e.PropertyName, error = e.ErrorMessage });

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}