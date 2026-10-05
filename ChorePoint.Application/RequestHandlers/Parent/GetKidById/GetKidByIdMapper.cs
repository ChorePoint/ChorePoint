using ChorePoint.Domain.Entities;

using Riok.Mapperly.Abstractions;

namespace ChorePoint.Application.RequestHandlers.Parent.GetKidById;

[Mapper]
public partial class GetKidByIdMapper
{
    public partial GetKidByIdResponse KidToGetKidByIdResponse(Kid kid);
}
