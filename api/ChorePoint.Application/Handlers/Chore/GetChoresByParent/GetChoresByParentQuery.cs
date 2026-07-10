using MediatR;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByParent;

public record GetChoresByParentQuery(bool? IsVisible)
    : IRequest<IReadOnlyList<GetChoresByParentResponse>>;
