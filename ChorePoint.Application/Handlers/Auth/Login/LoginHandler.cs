using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Auth.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IAppDbContext _context;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher<Parent> _passwordHasher;

    public LoginHandler(IAppDbContext context,
        IPasswordHasher<Parent> passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var parent = await _context.Parents
            .FirstOrDefaultAsync(p => p.Email.ToLower() == request.Email.ToLower(), cancellationToken);

        if (parent == null || _passwordHasher.VerifyHashedPassword(parent, parent.Password, request.Password) ==
            PasswordVerificationResult.Failed)
            throw new DomainException("Invalid email or password");

        var token = _jwtTokenGenerator.GenerateJwtToken(parent.Id, parent.Email);

        return new LoginResponse(token);
    }
}