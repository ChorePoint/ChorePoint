using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;

namespace ChorePoint.Application.Handlers.Chore.GetChoreById;

public class GetChoreByIdHandler : IRequestHandler<GetChoreByIdQuery, GetChoreByIdResponse>
{
    private readonly IAppDbContext _context;

    public GetChoreByIdHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<GetChoreByIdResponse> Handle(GetChoreByIdQuery request, CancellationToken cancellationToken)
    {
        var chore = await _context.Chores
            .FindAsync(request.Id, cancellationToken);

        return chore.Adapt<GetChoreByIdResponse>()
               ?? throw new NotFoundException($"No chores exist with id: {request.Id}");
    }
}