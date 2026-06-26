using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Representations;
using MediatR;

namespace ChorePoint.Application.Handlers.Shop.NewShopItem;

public record NewShopItemCommand(
    int? CategoryId,
    string Name,
    string Icon,
    string? Description,
    int Cost,
    int Quantity,
    IReadOnlyList<AssignedKidToShopItem> AssignedKids
) : IRequest;