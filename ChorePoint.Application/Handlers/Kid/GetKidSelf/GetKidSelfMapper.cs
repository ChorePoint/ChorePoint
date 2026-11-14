using Riok.Mapperly.Abstractions;

using KidE = ChorePoint.Domain.Entities.Kid;

namespace ChorePoint.Application.Handlers.Kid.GetKidSelf;

[Mapper]
public partial class GetKidSelfMapper
{
    public partial GetKidSelfResponse KidToGetKidSelfResponse(KidE kid);
}
