using MediatR;

namespace ChorePoint.Application.Handlers.Chore.DeleteChoreById;

public record DeleteChoreByIdCommand(
    int ChoreId
) : IRequest;