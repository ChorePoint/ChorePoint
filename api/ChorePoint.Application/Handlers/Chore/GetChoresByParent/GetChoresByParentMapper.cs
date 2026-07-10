using Riok.Mapperly.Abstractions;
using ChoreE = ChorePoint.Domain.Entities.Chore;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByParent;

[Mapper]
public partial class GetChoresByParentMapper
{
    public partial IReadOnlyList<GetChoresByParentResponse> ChoresToGetChoresByParentResponseList(
        IReadOnlyList<ChoreE> chores
    );

    [MapProperty(nameof(ChoreE.KidChores), nameof(GetChoresByParentResponse.AssignedKids))]
    private partial GetChoresByParentResponse ChoreToGetChoresByParentResponse(ChoreE chore);
}
