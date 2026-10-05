using MediatR;

namespace ChorePoint.Application.RequestHandlers.Shop.GetShopItemsByParent;

public record GetShopItemsByParentQuery(bool? IsVisible) : IRequest<IReadOnlyList<GetShopItemsByParentResponse>>;
