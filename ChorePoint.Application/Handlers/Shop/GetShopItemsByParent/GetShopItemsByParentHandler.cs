using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByParent;

public class GetShopItemsByParentHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<GetShopItemsByParentQuery, IReadOnlyList<GetShopItemsByParentResponse>>
{
    public async Task<IReadOnlyList<GetShopItemsByParentResponse>> Handle(GetShopItemsByParentQuery request, CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        if (!parentContextService.IsParent())
        {
            var shopOpeningDays = await context.ParentSettings
                .Where(ps => ps.ParentId.Equals(parentId))
                .Select(ps => ps.ShopOpeningDays)
                .SingleOrDefaultAsync(cancellationToken);

            if (shopOpeningDays is null)
            {
                throw new NotFoundException($"No ShopOpeningDays setting exists for parent ID [{parentId}]");
            }

            if (!shopOpeningDays.Contains(DateTime.UtcNow.DayOfWeek))
            {
                throw new DomainException($"Parent with ID [{parentId}] does not have today set as open for kids");
            }
        }

        var shopItems = await context.ShopItems
            .Include(si => si.Category)
            .Include(si => si.KidShopItems)
            .Where(si => si.ParentId.Equals(parentId))
            .Where(si => request.IsVisible == null || si.KidShopItems.Any(ksi => ksi.IsVisible.Equals(request.IsVisible)))
            .ToListAsync(cancellationToken);

        if (shopItems.Empty())
        {
            throw new NotFoundException($"No shop items exist for parent ID [{parentId}]");
        }

        var resourceParentIds = shopItems.Select(c => c.ParentId).ToList();
        AuthorisationHelper.EnsureParentOwnsAllResources(resourceParentIds, parentId);

        GetShopItemsByParentMapper mapper = new();
        return mapper.ShopItemsToGetShopItemsByParentResponseList(shopItems);
    }
}
