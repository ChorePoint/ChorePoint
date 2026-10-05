using Riok.Mapperly.Abstractions;

using ChoreSubmissionE = ChorePoint.Domain.Entities.ChoreSubmission;

namespace ChorePoint.Application.RequestHandlers.ChoreSubmission.GetSubmissionsByParent;

[Mapper]
public partial class GetSubmissionsByParentMapper
{
    public partial IReadOnlyList<GetSubmissionsByParentResponse> ChoreSubmissionsToGetSubmissionsByParentResponseList(
        IReadOnlyList<ChoreSubmissionE> choreSubmissions
    );

    private partial GetSubmissionsByParentResponse ChoreSubmissionToGetSubmissionsByParentResponse(ChoreSubmissionE choreSubmission);
}
