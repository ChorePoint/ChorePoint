using MediatR;

namespace ChorePoint.Application.Handlers.Auth.AddKidLoginCode;

public record AddKidLoginCodeCommand(int KidId) : IRequest;
