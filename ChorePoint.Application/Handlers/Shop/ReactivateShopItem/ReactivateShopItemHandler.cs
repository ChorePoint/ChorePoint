using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;

using MediatR;

namespace ChorePoint.Application.Handlers.Shop.ReactivateShopItem;

public class ReactivateShopItemHandler(IAppDbContext context, IParentContextService parentContextService) : IRequestHandler<ReactivateShopItemCommand>
{
    public async Task Handle(ReactivateShopItemCommand request, CancellationToken cancellationToken)
    {
        var shopItem = await context.ShopItems.FindAsync([request.ShopItemId], cancellationToken);

        if (shopItem is null)
        {
            throw new NotFoundException($"No shop item exists with ID [{request.ShopItemId}]");
        }

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(shopItem.ParentId, parentId);

        shopItem.Restock(request.Quantity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
