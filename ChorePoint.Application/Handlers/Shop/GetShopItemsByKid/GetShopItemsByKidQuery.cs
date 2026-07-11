using MediatR;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByKid;

public record GetShopItemsByKidQuery(int KidId)
    : IRequest<IReadOnlyList<GetShopItemsByKidResponse>>;
