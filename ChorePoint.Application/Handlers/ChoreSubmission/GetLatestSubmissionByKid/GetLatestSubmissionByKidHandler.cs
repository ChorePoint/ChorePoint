using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetLatestSubmissionByKid;

public class GetLatestSubmissionByKidHandler(IAppDbContext context, IParentContextService parentContextService, IKidContextService kidContextService)
    : IRequestHandler<GetLatestSubmissionByKidQuery, GetLatestSubmissionByKidResponse>
{
    public async Task<GetLatestSubmissionByKidResponse> Handle(GetLatestSubmissionByKidQuery request, CancellationToken cancellationToken)
    {
        var kidId = request.KidId == null ? kidContextService.GetKidId() : (int)request!.KidId!;

        var latestSubmission = await context.ChoreSubmissions
            .Include(cs => cs.Chore)
            .Where(cs => cs.KidId.Equals(kidId))
            .OrderByDescending(cs => cs.CompletedAt)
            .FirstOrDefaultAsync(cancellationToken);

        if (latestSubmission is null)
        {
            throw new NotFoundException($"No submission exists for kid ID [{kidId}]");
        }

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(latestSubmission.ParentId, parentId);

        GetLatestSubmissionByKidMapper mapper = new();
        return mapper.ChoreSubmissionToGetLatestSubmissionByKidResponse(latestSubmission);
    }
}
