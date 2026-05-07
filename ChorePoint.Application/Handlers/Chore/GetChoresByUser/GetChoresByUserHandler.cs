using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByUser;

public class GetChoresByUserHandler(IAppDbContext context)
    : IRequestHandler<GetChoresByUserQuery, IReadOnlyList<GetChoresByUserResponse>>
{
    public async Task<IReadOnlyList<GetChoresByUserResponse>> Handle(GetChoresByUserQuery request,
        CancellationToken cancellationToken)
    {
        var chores = await context.Chores
            .Where(c => c.UserId == request.Id)
            .ProjectToType<GetChoresByUserResponse>()
            .ToListAsync(cancellationToken);

        if (chores == null || chores.Count == 0)
            throw new NotFoundException($"No chores exist for user id: {request.Id}");

        return chores;
    }
}