using ChorePoint.Domain.Entities;

using Riok.Mapperly.Abstractions;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByKid;

[Mapper]
public partial class GetShopItemsByKidMapper
{
    public partial IReadOnlyList<GetShopItemsByKidResponse> ShopItemsToGetShopItemsByKidResponseList(IReadOnlyList<ShopItem> shopItems);

    [MapProperty(nameof(ShopItem.KidShopItems), nameof(GetShopItemsByKidResponse.PendingApproval), Use = nameof(KidShopItemsToPendingApproval))]
    [MapProperty(nameof(ShopItem.KidShopItems), nameof(GetShopItemsByKidResponse.IsVisible), Use = nameof(KidShopItemsToIsVisible))]
    private partial GetShopItemsByKidResponse ShopItemToGetShopItemsByKidResponse(ShopItem shopItem);

    [UserMapping]
    private static bool KidShopItemsToPendingApproval(ICollection<KidShopItem> kidShopItems)
    {
        var kidShopItem = kidShopItems.Single();
        return kidShopItem.PendingApproval;
    }

    [UserMapping]
    private static bool KidShopItemsToIsVisible(ICollection<KidShopItem> kidShopItems)
    {
        var kidShopItem = kidShopItems.Single();
        return kidShopItem.IsVisible;
    }
}
