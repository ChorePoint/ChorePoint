using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using MediatR;

namespace ChorePoint.Application.Handlers.Parent.CreateKid;

public class CreateKidHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<CreateKidCommand>
{
    public async Task Handle(CreateKidCommand request, CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        var kid = Kid.Create(parentId, request.Name, request.Avatar, request.Age);

        await context.Kids.AddAsync(kid, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
