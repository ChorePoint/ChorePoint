using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Auth.KidLogin;

public class KidLoginHandler(IAppDbContext context, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<KidLoginCommand, KidLoginResponse>
{
    public async Task<KidLoginResponse> Handle(KidLoginCommand request, CancellationToken cancellationToken)
    {
        var kid = await context.Kids
            .Where(k => k.Name.Equals(request.Name))
            .SingleOrDefaultAsync(k => k.LoginCode!.Equals(request.LoginCode), cancellationToken);

        if (kid is null)
        {
            throw new DomainException("Invalid name or login code");
        }

        var parent = await context.Parents.FindAsync([kid.ParentId], cancellationToken);

        if (parent is null)
        {
            throw new NotFoundException($"Parent with id [{kid.ParentId}] assigned to kid with id [{kid.KidId}] does not exist");
        }

        var token = jwtTokenGenerator.GenerateKidJwtToken(parent.ParentId, parent.Email);

        kid.LoginCode = null;
        await context.SaveChangesAsync(cancellationToken);

        return new KidLoginResponse(token);
    }
}
