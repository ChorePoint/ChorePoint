using MediatR;

namespace ChorePoint.Application.RequestHandlers.Chore.GetChoresByKid;

public record GetChoresByKidQuery(int KidId) : IRequest<IReadOnlyList<GetChoresByKidResponse>>;
