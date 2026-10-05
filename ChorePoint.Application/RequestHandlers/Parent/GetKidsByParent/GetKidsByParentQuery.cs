using MediatR;

namespace ChorePoint.Application.RequestHandlers.Parent.GetKidsByParent;

public record GetKidsByParentQuery : IRequest<IReadOnlyList<GetKidsByParentResponse>>;
