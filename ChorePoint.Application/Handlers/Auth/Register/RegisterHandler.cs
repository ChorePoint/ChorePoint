using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Auth.Register;

public class RegisterHandler(IAppDbContext context, IPasswordHasher<Domain.Entities.Parent> passwordHasher)
    : IRequestHandler<RegisterCommand>
{
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingParent = await context.Parents
            .FirstOrDefaultAsync(p => p.Email == request.Email, cancellationToken);
        
        if (existingParent != null)
            throw new ParentAlreadyExistsException(request.Email);

        var parent = new Domain.Entities.Parent
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