using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByKid;

public class GetShopItemsByKidHandler(
    IAppDbContext context,
    IParentContextService parentContextService)
    : IRequestHandler<GetShopItemsByKidQuery, IReadOnlyList<GetShopItemsByKidResponse>>
{
    public async Task<IReadOnlyList<GetShopItemsByKidResponse>> Handle(GetShopItemsByKidQuery request,
        CancellationToken cancellationToken)
    {
        var shopItems = await GetShopItemsByKidFromDb(request.KidId, cancellationToken);

        return shopItems.Empty()
            ? throw new NotFoundException($"No shop items are assigned to kid ID [{request.KidId}]")
            : shopItems.Adapt<IReadOnlyList<GetShopItemsByKidResponse>>();
    }

    private async Task<IReadOnlyList<ShopItem>> GetShopItemsByKidFromDb(int kidId, CancellationToken cancellationToken)
    {
        return await context.ShopItems
            .Where(si => si.KidId == kidId)
            .ToListAsync(cancellationToken);
    }
}