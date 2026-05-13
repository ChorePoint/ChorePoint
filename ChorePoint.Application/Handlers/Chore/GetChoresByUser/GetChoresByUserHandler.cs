using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByUser;

public class GetChoresByUserHandler(IAppDbContext context, IFusionCache cache)
    : IRequestHandler<GetChoresByUserQuery, IReadOnlyList<GetChoresByUserResponse>>
{
    public async Task<IReadOnlyList<GetChoresByUserResponse>> Handle(GetChoresByUserQuery request,
        CancellationToken cancellationToken)
    {
        var chores = await cache.GetOrSetAsync<IReadOnlyList<Domain.Entities.Chore>>(
            $"chores:{request.Id}",
            async _ => await GetChoresByUserIdFromDb(request, cancellationToken),
            token: cancellationToken
        );

        if (chores == null || chores.Count == 0)
            throw new NotFoundException($"No chores exist for user id: {request.Id}");

        return chores.Adapt<IReadOnlyList<GetChoresByUserResponse>>();
    }

    private async Task<IReadOnlyList<Domain.Entities.Chore>> GetChoresByUserIdFromDb(GetChoresByUserQuery request,
        CancellationToken cancellationToken)
    {
        var chores = await context.Chores
            .Where(c => c.UserId == request.Id)
            .ToListAsync(cancellationToken);

        return chores;
    }
}