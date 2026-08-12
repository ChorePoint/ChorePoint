using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using ParentE = ChorePoint.Domain.Entities.Parent;

namespace ChorePoint.Application.Handlers.Auth.ParentLogin;

public class ParentLoginHandler(IAppDbContext context, IPasswordHasher<ParentE> passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<ParentLoginCommand, ParentLoginResponse>
{
    public async Task<ParentLoginResponse> Handle(ParentLoginCommand request, CancellationToken cancellationToken)
    {
        var parent = await context.Parents.SingleOrDefaultAsync(
            p => p.Email.Equals(request.Email),
            cancellationToken
        );

        if (parent is null || passwordHasher.VerifyHashedPassword(parent, parent.Password, request.Password) == PasswordVerificationResult.Failed)
        {
            throw new DomainException("Invalid email or password");
        }

        var token = jwtTokenGenerator.GenerateParentJwtToken(parent.ParentId, parent.Email);

        return new ParentLoginResponse(token);
    }
}
