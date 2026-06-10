using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;

namespace ChorePoint.Application.Handlers.Shop.DeleteShopItem;

public class DeleteShopItemHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<DeleteShopItemCommand>
{
    public async Task Handle(DeleteShopItemCommand request, CancellationToken cancellationToken)
    {
        var shopItem = await context.ShopItems.FindAsync([request.ShopItemId], cancellationToken);

        if (shopItem is null)
            throw new NotFoundException($"No shop item exists with ID [{request.ShopItemId}]");

        var parentId = parentContextService.GetParentId();

        if (shopItem.ParentId != parentId)
            throw new DomainException(
                $"Shop item with assigned parent ID [{shopItem.ParentId}] does not belong to the logged in parent with ID [{parentId}]");

        context.ShopItems.Remove(shopItem);
        await context.SaveChangesAsync(cancellationToken);
    }
}