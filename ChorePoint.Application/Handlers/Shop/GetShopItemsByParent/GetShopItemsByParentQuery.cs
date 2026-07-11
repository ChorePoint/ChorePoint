using MediatR;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByParent;

public record GetShopItemsByParentQuery : IRequest<IReadOnlyList<GetShopItemsByParentResponse>>;
