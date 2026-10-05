using ChorePoint.Domain.Representations;

using MediatR;

namespace ChorePoint.Application.RequestHandlers.Shop.NewShopItem;

public record NewShopItemCommand(
    int? CategoryId,
    string Name,
    string Icon,
    string? Description,
    int Cost,
    int? Quantity,
    IReadOnlyList<AssignedKidToShopItem> AssignedKids
) : IRequest;
