using MediatR;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByUser;

public record GetChoresByUserQuery(
    int Id
) : IRequest<IReadOnlyList<GetChoresByUserResponse>>;