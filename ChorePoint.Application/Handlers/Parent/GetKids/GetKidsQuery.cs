using MediatR;

namespace ChorePoint.Application.Handlers.Parent.GetKids;

public record GetKidsQuery : IRequest<IReadOnlyCollection<GetKidsResponse>>;