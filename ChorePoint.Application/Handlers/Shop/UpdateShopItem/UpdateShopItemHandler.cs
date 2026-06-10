using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;

namespace ChorePoint.Application.Handlers.Shop.UpdateShopItem;

public class UpdateShopItemHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<UpdateShopItemCommand>
{
    public async Task Handle(UpdateShopItemCommand request, CancellationToken cancellationToken)
    {
        var shopItem = await context.ShopItems.FindAsync([request.ShopItemId], cancellationToken);

        if (shopItem is null)
            throw new NotFoundException($"No shop item exists with ID [{request.ShopItemId}]");

        var parentId = parentContextService.GetParentId();

        if (shopItem.ParentId != parentId)
            throw new DomainException(
                $"Shop item with assigned parent ID [{shopItem.ParentId}] does not belong to the logged in parent with ID [{parentId}]");

        shopItem.Update(request.Name, request.Cost, request.Status, request.Quantity);

        await context.SaveChangesAsync(cancellationToken);
    }
}