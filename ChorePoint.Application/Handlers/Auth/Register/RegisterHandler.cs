using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Auth.Register;

public class RegisterHandler(IAppDbContext context, IPasswordHasher<Parent> passwordHasher)
    : IRequestHandler<RegisterCommand>
{
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await context.Parents
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
        parent.Password = passwordHasher.HashPassword(parent, request.Password);

        context.Parents.Add(parent);
        await context.SaveChangesAsync(cancellationToken);
    }
}