using MediatR;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByUser;

public record GetChoresByParentQuery(
    bool visible
) : IRequest<IReadOnlyList<GetChoresByParentResponse>>;