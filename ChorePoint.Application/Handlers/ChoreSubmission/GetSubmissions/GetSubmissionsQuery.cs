using MediatR;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetSubmissions;
public record GetSubmissionsQuery(
    bool Pending
) : IRequest<IReadOnlyList<GetSubmissionsResponse>>;