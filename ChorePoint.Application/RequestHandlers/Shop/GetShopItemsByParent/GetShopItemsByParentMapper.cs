using ChorePoint.Domain.Entities;

using Riok.Mapperly.Abstractions;

namespace ChorePoint.Application.RequestHandlers.Shop.GetShopItemsByParent;

[Mapper]
public partial class GetShopItemsByParentMapper
{
    public partial IReadOnlyList<GetShopItemsByParentResponse> ShopItemsToGetShopItemsByParentResponseList(IReadOnlyList<ShopItem> shopItems);

    [MapProperty(nameof(ShopItem.KidShopItems), nameof(GetShopItemsByParentResponse.AssignedKids))]
    private partial GetShopItemsByParentResponse ShopItemToGetShopItemsByParentResponse(ShopItem shopItem);
}
