using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Users.GetKids;

public class GetKidsHandler(IAppDbContext context, IUserService userService)
    : IRequestHandler<GetKidsQuery, IReadOnlyCollection<GetKidsResponse>>
{
    public async Task<IReadOnlyCollection<GetKidsResponse>> Handle(GetKidsQuery request,
        CancellationToken cancellationToken)
    {
        var userId = userService.GetUserId();
        if (userId == null)
            throw new UnauthorizedAccessException("User not authorised");

        var kids = await context.Users
            .Where(u => u.ParentId == userId)
            .ProjectToType<GetKidsResponse>()
            .ToListAsync(cancellationToken);

        return kids.Count == 0
            ? throw new NotFoundException($"No kids exist for parent id: {userId}")
            : kids;
    }
}