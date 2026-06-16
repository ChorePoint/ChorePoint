using ChorePoint.Domain.Representations;
using MediatR;

namespace ChorePoint.Application.Handlers.Shop.NewShopItem;

public record NewShopItemCommand(
    string Name,
    int Cost,
    int Quantity,
    IReadOnlyList<AssignedKidToShopItem> AssignedKids
) : IRequest;