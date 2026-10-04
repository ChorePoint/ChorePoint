using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using ParentE = ChorePoint.Domain.Entities.Parent;

namespace ChorePoint.Application.Handlers.Auth.Register;

public class RegisterHandler(IAppDbContext context, IPasswordHasher<string> passwordHasher) : IRequestHandler<RegisterCommand>
{
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var emailAlreadyInUse = await context.Parents
            .AnyAsync(p => p.Email.Equals(request.Email), cancellationToken);

        if (emailAlreadyInUse)
        {
            throw new ParentAlreadyExistsException(request.Email);
        }

        var parent = ParentE.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            passwordHasher.HashPassword(string.Empty, request.Password)
        );
        parent.AddDefaultSettings(request.IanaTimeZone);

        await context.Parents.AddAsync(parent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
