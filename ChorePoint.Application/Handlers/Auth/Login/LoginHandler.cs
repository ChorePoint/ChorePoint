using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions.BaseException;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Auth.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IAppDbContext _context;
    private readonly IPasswordHasher<Parent> _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ICacheService _cacheService;
    
    public LoginHandler(IAppDbContext context,
        IPasswordHasher<Parent> passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        ICacheService cacheService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _cacheService = cacheService;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var parent = await _context.Parents
            .FirstOrDefaultAsync(p => p.Email.ToLower() == request.Email.ToLower(), cancellationToken);

        if (parent == null || _passwordHasher.VerifyHashedPassword(parent, parent.Password, request.Password) == PasswordVerificationResult.Failed)
        {
            throw new DomainException("Invalid email or password");
        }
        
        var token =  _jwtTokenGenerator.GenerateJwtToken(parent.Id, parent.Email);
        
        var cacheKey = $"parent:{parent.Id}";
        await _cacheService.SetAsync(cacheKey,
            new { parent.Id, parent.Email, parent.FirstName, parent.LastName },
            TimeSpan.FromHours(1), cancellationToken);

        return new LoginResponse(
            token
        );

    }
}