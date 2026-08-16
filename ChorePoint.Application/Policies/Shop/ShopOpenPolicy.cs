using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Policies.Shop;

public class ShopOpenPolicy(IAppDbContext context, IParentContextService parentContextService) : IShopOpenPolicy
{
    public async Task EnsureShopIsOpen(CancellationToken cancellationToken)
    {
        if (parentContextService.IsParent())
        {
            return;
        }

        var parentId = parentContextService.GetParentId();

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
}
