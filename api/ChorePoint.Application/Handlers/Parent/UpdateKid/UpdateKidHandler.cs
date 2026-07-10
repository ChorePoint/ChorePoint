using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;

namespace ChorePoint.Application.Handlers.Parent.UpdateKid;

public class UpdateKidHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<UpdateKidCommand>
{
    public async Task Handle(UpdateKidCommand request, CancellationToken cancellationToken)
    {
        var kid = await context.Kids
            .FindAsync([request.KidId], cancellationToken);

        if (kid is null)
            throw new NotFoundException($"No kid exists with ID [{request.KidId}]");

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(kid.ParentId, parentId);

        kid.Update(request.Name, request.Avatar, request.Age, request.DayStreak, request.LifetimePoints,
            request.SpendablePoints);

        await context.SaveChangesAsync(cancellationToken);
    }
}