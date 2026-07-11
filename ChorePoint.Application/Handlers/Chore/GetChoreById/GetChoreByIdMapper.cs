using Riok.Mapperly.Abstractions;
using ChoreE = ChorePoint.Domain.Entities.Chore;

namespace ChorePoint.Application.Handlers.Chore.GetChoreById;

[Mapper]
public partial class GetChoreByIdMapper
{
    [MapProperty(nameof(ChoreE.KidChores), nameof(GetChoreByIdResponse.AssignedKids))]
    public partial GetChoreByIdResponse ChoreToGetChoreByIdResponse(ChoreE chore);
}
