using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Auth.KidLogin;

public class KidLoginHandler(IAppDbContext context, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<KidLoginCommand, KidLoginResponse>
{
    public async Task<KidLoginResponse> Handle(KidLoginCommand request, CancellationToken cancellationToken)
    {
        var kid = await context.Kids.SingleOrDefaultAsync(k => k.Name.Equals(request.Name), cancellationToken);

        if (kid is null)
        {
            throw new DomainException("Invalid name or login code");
        }

        var loginCode = await context.LoginCodes.FindAsync([kid.KidId], cancellationToken);

        if (loginCode is null)
        {
            throw new DomainException("Invalid name or login code");
        }

        var parent = await context.Parents.FindAsync([kid.ParentId], cancellationToken);

        if (parent is null)
        {
            throw new NotFoundException($"Parent with ID [{kid.ParentId}] assigned to kid with ID [{kid.KidId}] does not exist");
        }

        context.LoginCodes.Remove(loginCode);
        await context.SaveChangesAsync(cancellationToken);

        var token = jwtTokenGenerator.GenerateKidJwtToken(parent.ParentId, parent.Email);
        return new KidLoginResponse(token);
    }
}
