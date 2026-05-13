using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByUser;

public class GetChoresByParentHandler(IAppDbContext context, IUserContextService userContextService)
    : IRequestHandler<GetChoresByParentQuery, IReadOnlyList<GetChoresByParentResponse>>
{
    public async Task<IReadOnlyList<GetChoresByParentResponse>> Handle(GetChoresByParentQuery request,
        CancellationToken cancellationToken)
    {

        var parentId = userContextService.GetParentId();

        var chores = await context.Chores
            .Where(c => c.User.ParentId == parentId)
            .Where(c => c.IsVisible == request.visible)
            .ProjectToType<GetChoresByParentResponse>()
            .ToListAsync(cancellationToken);

        if (chores == null || chores.Count == 0)
            throw new NotFoundException($"No chores exist for parent id: {parentId}");

        return chores;
    }
}