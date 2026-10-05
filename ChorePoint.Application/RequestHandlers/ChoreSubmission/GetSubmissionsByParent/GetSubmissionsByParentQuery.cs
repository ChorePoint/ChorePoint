using MediatR;

namespace ChorePoint.Application.RequestHandlers.ChoreSubmission.GetSubmissionsByParent;

public record GetSubmissionsByParentQuery(bool Pending) : IRequest<IReadOnlyList<GetSubmissionsByParentResponse>>;
