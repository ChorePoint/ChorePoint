using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Kid.GetKidSelf;

public class GetKidSelfHandler(IAppDbContext context, IKidContextService kidContextService) : IRequestHandler<GetKidSelfQuery, GetKidSelfResponse>
{
    public async Task<GetKidSelfResponse> Handle(GetKidSelfQuery request, CancellationToken cancellationToken)
    {
        var kidId = kidContextService.GetKidId();

        var kid = await context.Kids.Where(k => k.KidId.Equals(kidId)).FirstOrDefaultAsync(cancellationToken);

        if (kid == null)
        {
            throw new NotFoundException($"No kid exists of ID [{kidId}]");
        }

        GetKidSelfMapper mapper = new();
        return mapper.KidToGetKidSelfResponse(kid);
    }
}
