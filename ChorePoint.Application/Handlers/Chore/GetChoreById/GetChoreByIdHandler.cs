using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using ZiggyCreatures.Caching.Fusion;
using ChoreE = ChorePoint.Domain.Entities.Chore;

namespace ChorePoint.Application.Handlers.Chore.GetChoreById;

public class GetChoreByIdHandler(IAppDbContext context, IFusionCache cache)
    : IRequestHandler<GetChoreByIdQuery, GetChoreByIdResponse>
{
    public async Task<GetChoreByIdResponse> Handle(GetChoreByIdQuery request, CancellationToken cancellationToken)
    {
        var chore = await cache.GetOrSetAsync<ChoreE?>(
            $"chore:{request.ChoreId}",
            async _ => await GetChoreByIdFromDb(request.ChoreId, cancellationToken),
            token: cancellationToken
        );

        return chore.Adapt<GetChoreByIdResponse>()
               ?? throw new NotFoundException($"No chore exists with ID [{request.ChoreId}]");
    }

    private async Task<ChoreE?> GetChoreByIdFromDb(int choreId, CancellationToken cancellationToken)
    {
        return await context.Chores
            .FindAsync([choreId], cancellationToken);
    }
}