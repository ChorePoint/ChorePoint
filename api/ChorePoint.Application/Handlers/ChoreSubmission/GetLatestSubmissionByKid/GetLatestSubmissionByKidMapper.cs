using Riok.Mapperly.Abstractions;
using ChoreSubmissionE = ChorePoint.Domain.Entities.ChoreSubmission;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetLatestSubmissionByKid;

[Mapper]
public partial class GetLatestSubmissionByKidMapper
{
    public partial GetLatestSubmissionByKidResponse ChoreSubmissionToGetCurrentResponse(ChoreSubmissionE choreSubmission);
}