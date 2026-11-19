using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Application.Policies;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByKid;

file sealed class GetShopItemsByKidHandler(IAppDbContext context, IParentContextService parentContextService, IKidContextService kidContextService, IShopOperationPolicy shopOperationPolicy)
    : IRequestHandler<GetShopItemsByKidQuery, IReadOnlyList<GetShopItemsByKidResponse>>
{
    public async Task<IReadOnlyList<GetShopItemsByKidResponse>> Handle(GetShopItemsByKidQuery request, CancellationToken cancellationToken)
    {
        await shopOperationPolicy.EnsureShopOperationIsValidIfKid(ShopOperationToGate.Viewing, cancellationToken);

        var kidId = request.KidId == null ? kidContextService.GetKidId() : (int)request!.KidId!;

        var shopItems = await context.ShopItems
            .Include(si => si.Category)
            .Include(si => si.KidShopItems.Where(ksi => ksi.KidId.Equals(request.KidId)))
            .Where(si => si.KidShopItems.Any(ksi => ksi.KidId.Equals(kidId)))
            .ToListAsync(cancellationToken);

        if (shopItems.Empty())
        {
            throw new NotFoundException($"No shop items are assigned to kid ID [{kidId}]");
        }

        var resourceParentIds = shopItems.Select(si => si.ParentId).ToList();
        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsAllResources(resourceParentIds, parentId);

        GetShopItemsByKidMapper mapper = new();
        return mapper.ShopItemsToGetShopItemsByKidResponseList(shopItems);
    }
}
