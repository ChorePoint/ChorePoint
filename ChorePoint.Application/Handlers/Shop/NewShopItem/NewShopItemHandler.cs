using ChorePoint.Application.Handlers.Chore.Create;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.Shop.NewShopItem;

public class NewShopItemHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<NewShopItemCommand>
{
    public async Task Handle(NewShopItemCommand request, CancellationToken cancellationToken)
    {
        var existingKid = await context.Kids
            .FirstOrDefaultAsync(k => k.Id == request.KidId, cancellationToken);
        
        if (existingKid is null)
            throw new NotFoundException($"No kid exists with ID [{request.KidId}]");

        var parentId = parentContextService.GetParentId();

        if (existingKid.ParentId != parentId)
            throw new DomainException(
                $"Kid with assigned parent ID [{existingKid.ParentId}] does not belong to the logged in parent with ID [{parentId}]");

        var shopItem = ShopItem.Create
        (
            parentId,
            request.KidId,
            request.Name,
            request.Cost,
            DateTime.UtcNow
        );

        context.ShopItems.Add(shopItem);
        await context.SaveChangesAsync(cancellationToken);
    }
}