using ChorePoint.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ChorePoint.Application.Handlers.Parent.GetKids;

[Mapper]
public partial class GetKidsMapper
{
    public partial IReadOnlyList<GetKidsResponse> KidsToGetKidsResponseList(IReadOnlyList<Kid> kids);

    private partial GetKidsResponse KidToGetKidsResponse(Kid kid);
}