using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;

namespace ChorePoint.Application.Handlers.Chore.GetChoreById;

public class GetChoreByIdHandler(IAppDbContext context) : IRequestHandler<GetChoreByIdQuery, GetChoreByIdResponse>
{
    public async Task<GetChoreByIdResponse> Handle(GetChoreByIdQuery request, CancellationToken cancellationToken)
    {
        var chore = await context.Chores
            .FindAsync(request.Id, cancellationToken);

        return chore.Adapt<GetChoreByIdResponse>()
               ?? throw new NotFoundException($"No chores exist with id: {request.Id}");
    }
}