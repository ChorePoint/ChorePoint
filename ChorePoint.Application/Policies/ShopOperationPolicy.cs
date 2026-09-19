using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Policies;

internal interface IShopOperationPolicy
{
    Task EnsureShopOperationIsValidIfKid(ShopOperationToGate shopOperation, CancellationToken cancellationToken);
}

internal sealed class ShopOperationPolicy(IAppDbContext context, IParentContextService parentContextService) : IShopOperationPolicy
{
    public async Task EnsureShopOperationIsValidIfKid(ShopOperationToGate shopOperation, CancellationToken cancellationToken)
    {
        if (parentContextService.IsParent())
        {
            return;
        }

        var parentId = parentContextService.GetParentId();
        var parentSettings = await context.ParentSettings
            .Where(ps => ps.ParentId.Equals(parentId))
            .SingleOrDefaultAsync(cancellationToken);

        if (parentSettings is null)
        {
            throw new NotFoundException($"No settings exist for parent ID [{parentId}]");
        }

        if (!parentSettings.ShopOpeningDays.Contains(DateTime.UtcNow.DayOfWeek))
        {
            switch (shopOperation)
            {
                case ShopOperationToGate.Viewing:
                    if (!parentSettings.ClosedShopOnlyGatesPurchasing)
                    {
                        throw new DomainException($"Parent with ID [{parentId}] does not allow viewing shop items today");
                    }

                    break;
                case ShopOperationToGate.Purchasing:
                    if (parentSettings.ClosedShopOnlyGatesPurchasing)
                    {
                        throw new DomainException($"Parent with ID [{parentId}] does not allow purchasing shop items today");
                    }

                    break;
                default:
                    throw new ArgumentException($"Invalid operation [{shopOperation}] supplied");
            }
        }
    }
}
