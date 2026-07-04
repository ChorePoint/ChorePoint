using MediatR;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetSubmissionsByParent;

public record GetSubmissionsByParentQuery(
    bool Pending
) : IRequest<IReadOnlyList<GetSubmissionsByParentResponse>>;