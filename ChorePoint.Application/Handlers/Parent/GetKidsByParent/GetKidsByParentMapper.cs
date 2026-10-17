using Riok.Mapperly.Abstractions;

using KidE = ChorePoint.Domain.Entities.Kid;

namespace ChorePoint.Application.Handlers.Parent.GetKidsByParent;

[Mapper]
public partial class GetKidsByParentMapper
{
    public partial IReadOnlyList<GetKidsByParentResponse> KidsToGetKidsByParentResponseList(IReadOnlyList<KidE> kids);

    private partial GetKidsByParentResponse KidToGetKidsByParentResponse(KidE kid);
}
