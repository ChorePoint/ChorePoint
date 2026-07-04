using Riok.Mapperly.Abstractions;
using ChoreSubmissionE = ChorePoint.Domain.Entities.ChoreSubmission;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetSubmissions;

[Mapper]
public partial class GetSubmissionsMapper
{
    public partial IReadOnlyList<GetSubmissionsResponse> ChoreSubmissionsToGetSubmissionsResponseList(
        IReadOnlyList<ChoreSubmissionE> choreSubmissions);

    private partial GetSubmissionsResponse ChoreSubmissionToGetSubmissionsResponse(ChoreSubmissionE choreSubmission);
}