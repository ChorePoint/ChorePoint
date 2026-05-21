using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using ZiggyCreatures.Caching.Fusion;
using ParentE = ChorePoint.Domain.Entities.Parent;

namespace ChorePoint.Application.Handlers.Parent.GetMe;

public class GetMeHandler(IAppDbContext context, IParentContextService parentContextService, IFusionCache cache)
    : IRequestHandler<GetMeQuery, GetMeResponse>
{
    public async Task<GetMeResponse> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        var parent = await cache.GetOrSetAsync<ParentE?>(
            $"get_me:{parentId}",
            async _ => await GetParentFromDb(parentId, cancellationToken),
            token: cancellationToken
        );

        return parent.Adapt<GetMeResponse>()
               ?? throw new NotFoundException($"No parent exists with ID [{parentId}]");
    }

    private async Task<ParentE?> GetParentFromDb(int parentId, CancellationToken cancellationToken)
    {
        return await context.Parents
            .FindAsync([parentId], cancellationToken);
    }
}