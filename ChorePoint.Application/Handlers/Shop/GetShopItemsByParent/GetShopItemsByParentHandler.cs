using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByParent;

public class GetShopItemsByParentHandler(
    IAppDbContext context,
    IParentContextService parentContextService)
    : IRequestHandler<GetShopItemsByParentQuery, IReadOnlyList<GetShopItemsByParentResponse>>
{
    public async Task<IReadOnlyList<GetShopItemsByParentResponse>> Handle(GetShopItemsByParentQuery request,
        CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        var shopItems = await GetShopItemsByParentFromDb(parentId, cancellationToken);

        return shopItems.Empty()
            ? throw new NotFoundException($"No shop items are assigned to parent ID [{parentId}]")
            : shopItems.Adapt<IReadOnlyList<GetShopItemsByParentResponse>>();
    }

    private async Task<IReadOnlyList<ShopItem>> GetShopItemsByParentFromDb(int parentId,
        CancellationToken cancellationToken)
    {
        return await context.ShopItems
            .Where(si => si.ParentId == parentId)
            .ToListAsync(cancellationToken);
    }
}