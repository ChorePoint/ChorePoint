using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.Chore.GetChoreById;

public class GetChoreByIdHandler(IAppDbContext context, IFusionCache cache)
    : IRequestHandler<GetChoreByIdQuery, GetChoreByIdResponse>
{
    public async Task<GetChoreByIdResponse> Handle(GetChoreByIdQuery request, CancellationToken cancellationToken)
    {
        var chore = await cache.GetOrSetAsync<Domain.Entities.Chore?>(
            $"chore:{request.ChoreId}",
            async _ => await GetChoreByIdFromDb(request.ChoreId, cancellationToken),
            token: cancellationToken
        );

        return chore.Adapt<GetChoreByIdResponse>()
               ?? throw new NotFoundException($"No chores exist with ID: {request.ChoreId}");
    }

    private async Task<Domain.Entities.Chore?> GetChoreByIdFromDb(int choreId, CancellationToken cancellationToken)
    {
        var chore = await context.Chores
            .FindAsync([choreId], cancellationToken);

        return chore;
    }
}