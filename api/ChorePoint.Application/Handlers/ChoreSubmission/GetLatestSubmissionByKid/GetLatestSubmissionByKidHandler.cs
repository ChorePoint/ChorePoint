using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetLatestSubmissionByKid;

public class GetLatestSubmissionByKidHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<GetLatestSubmissionByKidQuery, GetLatestSubmissionByKidResponse>
{
    public async Task<GetLatestSubmissionByKidResponse> Handle(GetLatestSubmissionByKidQuery request, CancellationToken cancellationToken)
    {
        var currentSubmission = await context.ChoreSubmissions
            .Include(cs => cs.Chore)
            .Where(cs => cs.KidId.Equals(request.KidId))
            .OrderByDescending(cs => cs.CompletedAt)
            .FirstOrDefaultAsync(cancellationToken);

        if (currentSubmission is null)
            throw new NotFoundException($"No submission exists for kid ID [{request.KidId}]");

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(currentSubmission.ParentId, parentId);

        var mapper = new GetLatestSubmissionByKidMapper();
        return mapper.ChoreSubmissionToGetCurrentResponse(currentSubmission);
    }
}