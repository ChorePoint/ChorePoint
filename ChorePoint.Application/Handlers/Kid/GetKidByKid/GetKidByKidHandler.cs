using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Kid.GetKidByKid;

public class GetSelfHandler(IAppDbContext context, IKidContextService kidContextService)
    : IRequestHandler<GetKidByKidQuery, GetKidByKidResponse>
{
    public async Task<GetKidByKidResponse> Handle(GetKidByKidQuery request, CancellationToken cancellationToken)
    {
        var kidId = kidContextService.GetKidId();

        var kid = await context.Kids.Where(k => k.KidId == kidId).FirstOrDefaultAsync(cancellationToken);

        if (kid is null)
        {
            throw new NotFoundException($"No kid exists with ID [{kidId}]");
        }

        GetKidByKidMapper mapper = new();
        return mapper.KidToGetByKidResponse(kid);
    }
}
