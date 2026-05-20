using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByKid;

public class GetChoresByKidHandler(IAppDbContext context, IFusionCache cache)
    : IRequestHandler<GetChoresByKidQuery, IReadOnlyList<GetChoresByKidResponse>>
{
    public async Task<IReadOnlyList<GetChoresByKidResponse>> Handle(GetChoresByKidQuery request,
        CancellationToken cancellationToken)
    {
        var chores = await cache.GetOrSetAsync<IReadOnlyList<Domain.Entities.Chore>>(
            $"get_chores_by_kid:{request.KidId}",
            async _ => await GetChoresByKidIdFromDb(request.KidId, cancellationToken),
            token: cancellationToken
        );

        if (chores == null || chores.Count == 0)
            throw new NotFoundException($"No chores exist with kid ID [{request.KidId}]");

        return chores.Adapt<IReadOnlyList<GetChoresByKidResponse>>();
    }

    private async Task<IReadOnlyList<Domain.Entities.Chore>> GetChoresByKidIdFromDb(int kidId,
        CancellationToken cancellationToken)
    {
        return await context.Chores
            .Where(c => c.KidId == kidId)
            .ToListAsync(cancellationToken);
    }
}