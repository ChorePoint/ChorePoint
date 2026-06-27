using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetCurrent;

public class GetCurrentHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<GetCurrentQuery, GetCurrentResponse>
{
    public async Task<GetCurrentResponse> Handle(GetCurrentQuery request, CancellationToken cancellationToken)
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

        var mapper = new GetCurrentMapper();
        return mapper.ChoreSubmissionToGetCurrentResponse(currentSubmission);
    }
}