using MediatR;

namespace ChorePoint.Application.RequestHandlers.Chore.GetChoresByParent;

public record GetChoresByParentQuery(bool? IsVisible) : IRequest<IReadOnlyList<GetChoresByParentResponse>>;
