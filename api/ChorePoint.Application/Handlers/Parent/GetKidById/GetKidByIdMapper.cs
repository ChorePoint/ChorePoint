using ChorePoint.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ChorePoint.Application.Handlers.Parent.GetKidById;

[Mapper]
public partial class GetKidByIdMapper
{
    public partial GetKidByIdResponse KidToGetKidByIdResponse(Kid kid);
}