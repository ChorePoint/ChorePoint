using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Shop.UpdateShopItem;

public class UpdateShopItemHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<UpdateShopItemCommand>
{
    public async Task Handle(UpdateShopItemCommand request, CancellationToken cancellationToken)
    {
        var shopItem = await context.ShopItems
            .Include(si  => si.KidShopItems)
            .FirstOrDefaultAsync(c => c.ShopItemId.Equals(request.ShopItemId), cancellationToken);

        if (shopItem is null)
            throw new NotFoundException($"No shop item exists with ID [{request.ShopItemId}]");

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(shopItem.ParentId, parentId);

        shopItem.Update(request.CategoryId, request.Name, request.Icon, request.Description, request.Cost, request.Quantity);

        foreach (var assignedKid in request.AssignedKids)
        {
            // Can be SingleOrDefault() as each kid cannot be assigned to the same chore multiple times
            var kidShopItem = shopItem.KidShopItems.SingleOrDefault(kc => kc.KidId.Equals(assignedKid.KidId));
            
            if (kidShopItem is null)
                throw new DomainException($"Kid with ID [{assignedKid.KidId}] is not assigned to shop item with ID [{request.ShopItemId}]");
            
            kidShopItem.Update(assignedKid.Status);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}