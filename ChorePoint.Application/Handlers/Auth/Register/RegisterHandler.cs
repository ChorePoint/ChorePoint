using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Auth.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand>
{
    private readonly IAppDbContext _context;
    private readonly IPasswordHasher<Parent> _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ICacheService _cacheService;
    
    public RegisterHandler(IAppDbContext context,
        IPasswordHasher<Parent> passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        ICacheService cacheService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _cacheService = cacheService;
    }

    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _context.Parents
            .FirstOrDefaultAsync(p => p.Email == request.Email, cancellationToken);
        if (existingUser != null)
            throw new UserAlreadyExistsException(request.Email);

        var parent = new Parent
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow
        };
        parent.Password = _passwordHasher.HashPassword(parent, request.Password);

        _context.Parents.Add(parent);
        await _context.SaveChangesAsync(cancellationToken);
        
        var cacheKey = $"parent:{parent.Id}";
        await _cacheService.SetAsync(cacheKey,
            new { parent.Id, parent.FirstName, parent.LastName, parent.Email },
            TimeSpan.FromHours(1), cancellationToken);
    }
}