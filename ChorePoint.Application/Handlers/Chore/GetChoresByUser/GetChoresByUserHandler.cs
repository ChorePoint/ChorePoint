using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByUser;

public class GetChoresByUserHandler : IRequestHandler<GetChoresByUserQuery, IReadOnlyList<GetChoresByUserResponse>>
{
    private readonly IAppDbContext _context;
    
    public GetChoresByUserHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<GetChoresByUserResponse>> Handle(GetChoresByUserQuery request, CancellationToken cancellationToken)
    {
        var chores = await _context.Chores
            .Where(c => c.UserId == request.Id)
            .ProjectToType<GetChoresByUserResponse>()
            .ToListAsync(cancellationToken);
        
        if (chores == null || chores.Count == 0)
            throw new NotFoundException($"No chores exist for user id: {request.Id}");
        
        return chores;
    }
}