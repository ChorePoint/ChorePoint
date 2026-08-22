using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;

using MediatR;

namespace ChorePoint.Application.Handlers.Auth.AddKidLoginCode;

public class AddKidLoginCodeHandler(IAppDbContext context, IParentContextService parentContextService, IKidLoginCodeGenerator kidLoginCodeGenerator)
    : IRequestHandler<AddKidLoginCodeCommand>
{
    public async Task Handle(AddKidLoginCodeCommand request, CancellationToken cancellationToken)
    {
        var kid = await context.Kids.FindAsync([request.KidId], cancellationToken);

        if (kid is null)
        {
            throw new NotFoundException($"No kid exists with ID [{request.KidId}]");
        }

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(kid.ParentId, parentId);

        var loginCodeString = kidLoginCodeGenerator.GenerateLoginCode();
        var loginCode = LoginCode.Create(kid.KidId, loginCodeString);

        await context.LoginCodes.AddAsync(loginCode, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
