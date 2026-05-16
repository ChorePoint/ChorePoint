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
    IUserContextService userContextService,
    IFusionCache cache)
    : IRequestHandler<GetSubmissionsQuery, IReadOnlyList<GetSubmissionsResponse>>
{
    public async Task<IReadOnlyList<GetSubmissionsResponse>> Handle(GetSubmissionsQuery request,
        CancellationToken cancellationToken)
    {
        var parentId = userContextService.GetParentId();

        var choreSubmissions = await cache.GetOrSetAsync<IReadOnlyList<Domain.Entities.ChoreSubmission>>(
            $"get_chore_submissions:{parentId}:{request.Pending}",
            async _ => await GetSubmissionsForUserFromDb(parentId, request.Pending, cancellationToken),
            token: cancellationToken
        );

        if (choreSubmissions == null || choreSubmissions.Count == 0)
            throw new NotFoundException($"No pending chore submissions found for user ID: {parentId}");

        return choreSubmissions.Adapt<IReadOnlyList<GetSubmissionsResponse>>();
    }

    private async Task<IReadOnlyList<Domain.Entities.ChoreSubmission>> GetSubmissionsForUserFromDb(
        int userId,
        bool pending,
        CancellationToken cancellationToken)
    {
        var query = context.ChoreSubmissions
            .Where(c => c.UserId == userId);

        if (pending) query = query.Where(c => c.ApprovalStatus == ChoreApprovalStatus.Pending);

        return await query.ToListAsync(cancellationToken);
    }
}