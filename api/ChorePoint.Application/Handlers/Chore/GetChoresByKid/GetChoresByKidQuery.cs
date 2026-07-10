using MediatR;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByKid;

public record GetChoresByKidQuery(int KidId) : IRequest<IReadOnlyList<GetChoresByKidResponse>>;
