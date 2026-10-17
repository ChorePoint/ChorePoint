using Riok.Mapperly.Abstractions;

namespace ChorePoint.Application.Handlers.Kid.GetKidByKid;

using KidE = ChorePoint.Domain.Entities.Kid;

[Mapper]
public partial class GetKidByKidMapper
{
    public partial GetKidByKidResponse KidToGetByKidResponse(KidE kid);
}
