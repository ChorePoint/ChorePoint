using ChorePoint.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ChorePoint.Application.Handlers.Parent.GetKidsByParent;

[Mapper]
public partial class GetKidsByParentMapper
{
    public partial IReadOnlyList<GetKidsByParentResponse> KidsToGetKidsByParentResponseList(IReadOnlyList<Kid> kids);

    private partial GetKidsByParentResponse KidToGetKidsByParentResponse(Kid kid);
}