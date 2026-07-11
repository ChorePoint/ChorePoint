using MediatR;

namespace ChorePoint.Application.Handlers.Auth.Register;

public record RegisterCommand(string FirstName, string LastName, string Email, string Password)
    : IRequest;
