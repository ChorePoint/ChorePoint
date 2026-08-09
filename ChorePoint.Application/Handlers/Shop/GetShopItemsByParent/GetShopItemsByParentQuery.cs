using MediatR;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByParent;

public record GetShopItemsByParentQuery(bool? IsVisible) : IRequest<IReadOnlyList<GetShopItemsByParentResponse>>;
