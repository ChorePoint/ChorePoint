using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Enums;
using Riok.Mapperly.Abstractions;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByKid;

[Mapper]
public partial class GetShopItemsByKidMapper
{
    public partial IReadOnlyList<GetShopItemsByKidResponse> ShopItemsToGetShopItemsByKidResponseList(
        IReadOnlyList<ShopItem> shopItems
    );

    [MapProperty(nameof(ShopItem.KidShopItems), nameof(GetShopItemsByKidResponse.Status))]
    private partial GetShopItemsByKidResponse ShopItemToGetShopItemsByKidResponse(
        ShopItem shopItem
    );

    [UserMapping]
    private static ShopItemStatus KidShopItemsToStatus(ICollection<KidShopItem> kidShopItems)
    {
        var kidShopItem = kidShopItems.Single();
        return kidShopItem.Status;
    }
}
