using Riok.Mapperly.Abstractions;

using KidE = ChorePoint.Domain.Entities.Kid;

namespace ChorePoint.Application.Handlers.Parent.GetKidById;

[Mapper]
public partial class GetKidByIdMapper
{
    public partial GetKidByIdResponse KidToGetKidByIdResponse(KidE kid);
}
