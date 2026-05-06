using MediatR;

namespace ChorePoint.Application.Handlers.Users.GetKids;

public record GetKidsQuery() : IRequest<IReadOnlyCollection<GetKidsResponse>>;