using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.Parent.GetKidById;

public class GetKidByIdHandler(IAppDbContext context, IParentContextService parentContextService, IFusionCache cache)
    : IRequestHandler<GetKidByIdQuery, GetKidByIdResponse>
{
    public async Task<GetKidByIdResponse> Handle(GetKidByIdQuery request, CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        var kid = await cache.GetOrSetAsync(
            $"kid:{request.KidId}",
            async _ => await GetKidByIdFromDb(request.KidId, cancellationToken),
            token: cancellationToken
        );

        return kid.Adapt<GetKidByIdResponse>()
               ?? throw new NotFoundException($"No kid exists with ID [{request.KidId}]");
    }

    private async Task<Kid?> GetKidByIdFromDb(int kidId, CancellationToken cancellationToken)
    {
        return await context.Kids
            .FindAsync([kidId], cancellationToken);
    }
}