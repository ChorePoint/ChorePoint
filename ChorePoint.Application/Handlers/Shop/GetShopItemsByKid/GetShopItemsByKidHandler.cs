using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByKid;

public class GetShopItemsByKidHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<GetShopItemsByKidQuery, IReadOnlyList<GetShopItemsByKidResponse>>
{
    public async Task<IReadOnlyList<GetShopItemsByKidResponse>> Handle(GetShopItemsByKidQuery request,
        CancellationToken cancellationToken)
    {
        var shopItems = await context.ShopItems
            .Include(si => si.Category)
            .Include(si => si.KidShopItems)
            .Where(si => si.KidShopItems.Any(ksi => ksi.KidId.Equals(request.KidId)))
            .ToListAsync(cancellationToken);

        if (shopItems.Empty())
            throw new NotFoundException($"No shop items are assigned to kid ID [{request.KidId}]");

        var resourceParentIds = shopItems.Select(si => si.ParentId).ToList();
        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsAllResources(resourceParentIds, parentId);

        return shopItems.Adapt<IReadOnlyList<GetShopItemsByKidResponse>>();
    }
}