using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ParentE = ChorePoint.Domain.Entities.Parent;

namespace ChorePoint.Application.Handlers.Auth.Register;

public class RegisterHandler(IAppDbContext context, IPasswordHasher<ParentE> passwordHasher)
    : IRequestHandler<RegisterCommand>
{
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingParent = await context.Parents
            .SingleOrDefaultAsync(p => p.Email.Equals(request.Email), cancellationToken);

        if (existingParent is not null)
            throw new ParentAlreadyExistsException(request.Email);

        var parent = ParentE.CreateWithoutPassword
        (
            request.FirstName,
            request.LastName,
            request.Email
        );
        parent.SetPassword(passwordHasher.HashPassword(parent, request.Password));

        await context.Parents.AddAsync(parent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}