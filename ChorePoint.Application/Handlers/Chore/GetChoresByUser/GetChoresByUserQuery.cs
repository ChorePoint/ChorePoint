using MediatR;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByUser;

public record GetChoresByUserQuery(
    int UserId
) : IRequest<IReadOnlyList<GetChoresByUserResponse>>;