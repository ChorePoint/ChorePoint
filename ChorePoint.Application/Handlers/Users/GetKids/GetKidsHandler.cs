using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Users.GetKids;

public class GetKidsHandler(IAppDbContext context, IUserContextService userContextService)
    : IRequestHandler<GetKidsQuery, IReadOnlyCollection<GetKidsResponse>>
{
    public async Task<IReadOnlyCollection<GetKidsResponse>> Handle(GetKidsQuery request,
        CancellationToken cancellationToken)
    {
        var parentId = userContextService.GetParentId();

        var kids = await context.Users
            .Where(u => u.ParentId == parentId)
            .ProjectToType<GetKidsResponse>()
            .ToListAsync(cancellationToken);

        return kids.Count == 0
            ? throw new NotFoundException($"No kids exist for parent id: {parentId}")
            : kids;
    }
}