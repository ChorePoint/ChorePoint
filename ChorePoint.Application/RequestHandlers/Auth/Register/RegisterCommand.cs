using MediatR;

namespace ChorePoint.Application.RequestHandlers.Auth.Register;

public record RegisterCommand(string FirstName, string LastName, string Email, string Password, string? IanaTimeZone) : IRequest;
