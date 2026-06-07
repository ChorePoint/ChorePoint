using ChorePoint.Application.Handlers.Chore.GetChoresByParent;
using MediatR;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByParent;

public record GetShopItemsByParentQuery : IRequest<IReadOnlyList<GetShopItemsByParentResponse>>;