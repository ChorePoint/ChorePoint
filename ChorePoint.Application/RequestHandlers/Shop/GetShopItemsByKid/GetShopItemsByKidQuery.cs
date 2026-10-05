using MediatR;

namespace ChorePoint.Application.RequestHandlers.Shop.GetShopItemsByKid;

public record GetShopItemsByKidQuery(int KidId) : IRequest<IReadOnlyList<GetShopItemsByKidResponse>>;
