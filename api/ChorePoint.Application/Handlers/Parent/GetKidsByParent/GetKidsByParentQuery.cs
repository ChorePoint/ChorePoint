using MediatR;

namespace ChorePoint.Application.Handlers.Parent.GetKidsByParent;

public record GetKidsByParentQuery : IRequest<IReadOnlyList<GetKidsByParentResponse>>;
