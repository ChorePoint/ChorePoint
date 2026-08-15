using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByKid;

public class GetShopItemsByKidHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<GetShopItemsByKidQuery, IReadOnlyList<GetShopItemsByKidResponse>>
{
    public async Task<IReadOnlyList<GetShopItemsByKidResponse>> Handle(GetShopItemsByKidQuery request, CancellationToken cancellationToken)
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
            .Include(si => si.KidShopItems.Where(ksi => ksi.KidId.Equals(request.KidId)))
            .Where(si => si.KidShopItems.Any(ksi => ksi.KidId.Equals(request.KidId)))
            .ToListAsync(cancellationToken);

        if (shopItems.Empty())
        {
            throw new NotFoundException($"No shop items are assigned to kid ID [{request.KidId}]");
        }

        var resourceParentIds = shopItems.Select(si => si.ParentId).ToList();
        AuthorisationHelper.EnsureParentOwnsAllResources(resourceParentIds, parentId);

        GetShopItemsByKidMapper mapper = new();
        return mapper.ShopItemsToGetShopItemsByKidResponseList(shopItems);
    }
}
