using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Shop.UpdateShopItem;

public class UpdateShopItemHandler(IAppDbContext context, IParentContextService parentContextService) : IRequestHandler<UpdateShopItemCommand>
{
    public async Task Handle(UpdateShopItemCommand request, CancellationToken cancellationToken)
    {
        var shopItem = await context.ShopItems
            .Include(si => si.KidShopItems)
            .SingleOrDefaultAsync(c => c.ShopItemId.Equals(request.ShopItemId), cancellationToken);

        if (shopItem is null)
        {
            throw new NotFoundException($"No shop item exists with ID [{request.ShopItemId}]");
        }

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(shopItem.ParentId, parentId);

        shopItem.Update(
            request.CategoryId,
            request.Name,
            request.Icon,
            request.Description,
            request.Cost,
            request.Quantity
        );

        shopItem.KidShopItems.Clear();
        foreach (var assignedKid in request.AssignedKids)
        {
            var kidShopItem = KidShopItem.Create(assignedKid.KidId);
            shopItem.KidShopItems.Add(kidShopItem);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
