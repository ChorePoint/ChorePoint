using Riok.Mapperly.Abstractions;
using ChoreSubmissionE = ChorePoint.Domain.Entities.ChoreSubmission;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetCurrent;

[Mapper]
public partial class GetCurrentMapper
{
    public partial GetCurrentResponse ChoreSubmissionToGetCurrentResponse(ChoreSubmissionE choreSubmission);
}