using MediatR;

namespace ChorePoint.Application.Handlers.Shop.NewShopItem;

public record NewShopItemCommand(
    string Name,
    int Cost,
    int Quantity,
    IReadOnlyList<int> KidIds
) : IRequest;