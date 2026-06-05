using ChorePoint.Domain.Enums;
using MediatR;

namespace ChorePoint.Application.Handlers.Shop.NewShopItem;

public record NewShopItemCommand(
    int KidId,
    string Name,
    int Cost
) : IRequest;