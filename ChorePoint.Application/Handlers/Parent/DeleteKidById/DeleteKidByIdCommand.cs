using MediatR;

namespace ChorePoint.Application.Handlers.Parent.DeleteKidById;

public record DeleteKidByIdCommand(
    int KidId
) : IRequest;