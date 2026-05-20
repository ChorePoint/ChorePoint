using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetSubmissions;

public class GetSubmissionsHandler(
    IAppDbContext context,
    IParentContextService parentContextService,
    IFusionCache cache)
    : IRequestHandler<GetSubmissionsQuery, IReadOnlyList<GetSubmissionsResponse>>
{
    public async Task<IReadOnlyList<GetSubmissionsResponse>> Handle(GetSubmissionsQuery request,
        CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        var choreSubmissions = await cache.GetOrSetAsync<IReadOnlyList<Domain.Entities.ChoreSubmission>>(
            $"get_submissions:{parentId}:{request.Pending}",
            async _ => await GetSubmissionsFromParentFromDb(parentId, request.Pending, cancellationToken),
            token: cancellationToken
        );

        if (choreSubmissions.Count == 0)
        {
            var pendingText = request.Pending ? "pending" : string.Empty;
            throw new NotFoundException($"No {pendingText} submissions found with parent ID [{parentId}]");
        }

        return choreSubmissions.Adapt<IReadOnlyList<GetSubmissionsResponse>>();
    }

    private async Task<IReadOnlyList<Domain.Entities.ChoreSubmission>> GetSubmissionsFromParentFromDb(
        int parentId,
        bool pending,
        CancellationToken cancellationToken)
    {
        var query = context.ChoreSubmissions
            .Include(cs => cs.Chore)
            .Where(cs => cs.Kid.ParentId == parentId);

        if (pending) query = query.Where(cs => cs.ApprovalStatus == ChoreApprovalStatus.Pending);

        return await query.ToListAsync(cancellationToken);
    }
}