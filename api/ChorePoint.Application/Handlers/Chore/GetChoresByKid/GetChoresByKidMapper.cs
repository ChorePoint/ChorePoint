using ChorePoint.Domain.Entities;
using Riok.Mapperly.Abstractions;
using ChoreE = ChorePoint.Domain.Entities.Chore;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByKid;

[Mapper]
public partial class GetChoresByKidMapper
{
    public partial IReadOnlyList<GetChoresByKidResponse> ChoresToGetChoresByKidResponseList(
        IReadOnlyList<ChoreE> chores
    );

    [MapProperty(nameof(ChoreE.KidChores), nameof(GetChoresByKidResponse.DueDay))]
    [MapProperty(nameof(ChoreE.KidChores), nameof(GetChoresByKidResponse.IsVisible))]
    private partial GetChoresByKidResponse ChoreToGetChoresByKidResponse(ChoreE chore);

    [UserMapping]
    private static DayOfWeek? KidChoresToDueDay(ICollection<KidChore> kidChores)
    {
        var kidChore = kidChores.Single();
        return kidChore.DueDay;
    }

    [UserMapping]
    private static bool KidChoresToIsVisible(ICollection<KidChore> kidChores)
    {
        var kidChore = kidChores.Single();
        return kidChore.IsVisible;
    }
}
