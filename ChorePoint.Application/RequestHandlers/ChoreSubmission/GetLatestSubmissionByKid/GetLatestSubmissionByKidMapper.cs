using Riok.Mapperly.Abstractions;

using ChoreSubmissionE = ChorePoint.Domain.Entities.ChoreSubmission;

namespace ChorePoint.Application.RequestHandlers.ChoreSubmission.GetLatestSubmissionByKid;

[Mapper]
public partial class GetLatestSubmissionByKidMapper
{
    public partial GetLatestSubmissionByKidResponse ChoreSubmissionToGetLatestSubmissionByKidResponse(ChoreSubmissionE choreSubmission);
}
