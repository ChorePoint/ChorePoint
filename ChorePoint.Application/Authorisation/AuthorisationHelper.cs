using ChorePoint.Domain.Exceptions;

namespace ChorePoint.Application.Authorisation;

public static class AuthorisationHelper
{
    public static void EnsureAssignedKidIdsAreValid(IReadOnlyList<int> resourceParentIds,
        IReadOnlyList<int> assignedKidIds)
    {
        if (resourceParentIds.Count != assignedKidIds.Count)
            throw new NotFoundException("One or multiple of the assigned kid IDs does not exist");
    }

    public static void EnsureParentOwnsResource(int resourceParentId, int parentIdFromContext)
    {
        if (resourceParentId != parentIdFromContext) throw new ParentNotAuthorisedException(parentIdFromContext);
    }

    public static void EnsureParentOwnsAllResources(IReadOnlyList<int> resourceParentIds, int parentIdFromContext)
    {
        if (resourceParentIds.Any(resourceParentId => resourceParentId != parentIdFromContext))
            throw new ParentNotAuthorisedException(parentIdFromContext);
    }
}