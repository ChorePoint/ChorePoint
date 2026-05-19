using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Auth.Login;

public class LoginHandler(
    IAppDbContext context,
    IPasswordHasher<Domain.Entities.Parent> passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var parent = await context.Parents
            .FirstOrDefaultAsync(p => p.Email.Equals(request.Email), cancellationToken);

        if (parent == null || passwordHasher.VerifyHashedPassword(parent, parent.Password, request.Password) ==
            PasswordVerificationResult.Failed)
            throw new DomainException("Invalid email or password");

        var token = jwtTokenGenerator.GenerateJwtToken(parent.Id, parent.Email);

        return new LoginResponse(token);
    }
}