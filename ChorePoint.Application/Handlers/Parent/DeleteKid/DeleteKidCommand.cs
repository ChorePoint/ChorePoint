using MediatR;

namespace ChorePoint.Application.Handlers.Parent.DeleteKid;

public record DeleteKidCommand(
    int KidId
) : IRequest;