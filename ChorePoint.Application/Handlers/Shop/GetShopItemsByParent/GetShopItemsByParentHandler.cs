using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByParent;

public class GetShopItemsByParentHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<GetShopItemsByParentQuery, IReadOnlyList<GetShopItemsByParentResponse>>
{
    public async Task<IReadOnlyList<GetShopItemsByParentResponse>> Handle(GetShopItemsByParentQuery request,
        CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        var shopItems = await context.ShopItems
            .Include(si => si.Category)
            .Include(si => si.KidShopItems)
            .Where(si => si.ParentId.Equals(parentId))
            .ToListAsync(cancellationToken);

        if (shopItems.Empty())
            throw new NotFoundException($"No shop items exist for parent ID [{parentId}]");

        var resourceParentIds = shopItems.Select(c => c.ParentId).ToList();
        AuthorisationHelper.EnsureParentOwnsAllResources(resourceParentIds, parentId);

        return shopItems.Adapt<IReadOnlyList<GetShopItemsByParentResponse>>();
    }
}