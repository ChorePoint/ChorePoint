using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using MediatR;

namespace ChorePoint.Application.Handlers.Parent.UpdateKid;

public class UpdateKidHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<UpdateKidCommand>
{
    public async Task Handle(UpdateKidCommand request, CancellationToken cancellationToken)
    {
        var kid = await GetKidByIdFromDb(request.Id, cancellationToken);

        if (kid is null)
            throw new NotFoundException($"No kid exists with ID [{request.Id}]");

        var parentId = parentContextService.GetParentId();

        if (kid.ParentId != parentId)
            throw new DomainException(
                $"Kid with assigned parent ID [{kid.ParentId}] does not belong to the logged in parent with ID [{parentId}]");

        kid.Update(
            request.Name,
            request.Age,
            request.Avatar,
            request.SpendablePoints,
            request.DayStreak);

        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Kid?> GetKidByIdFromDb(int kidId, CancellationToken cancellationToken)
    {
        return await context.Kids
            .FindAsync([kidId], cancellationToken);
    }
}