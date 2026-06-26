using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Shop.NewShopItem;

public class NewShopItemHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<NewShopItemCommand>
{
    public async Task Handle(NewShopItemCommand request, CancellationToken cancellationToken)
    {
        var assignedKidIds = request.AssignedKids.Select(ak => ak.KidId).ToList();
        var resourceParentIds = await context.Kids
            .Where(k => assignedKidIds.Contains(k.KidId))
            .Select(k => k.ParentId)
            .ToListAsync(cancellationToken);

        AuthorisationHelper.EnsureAssignedKidIdsAreValid(resourceParentIds, assignedKidIds);

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsAllResources(resourceParentIds, parentId);

        var shopItem = ShopItem.Create
        (
            parentId,
            request.CategoryId,
            request.Name,
            request.Icon,
            request.Description,
            request.Cost,
            request.Quantity
        );

        foreach (var assignedKid in request.AssignedKids)
            shopItem.KidShopItems.Add(KidShopItem.Create(assignedKid.KidId));

        await context.ShopItems.AddAsync(shopItem, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}