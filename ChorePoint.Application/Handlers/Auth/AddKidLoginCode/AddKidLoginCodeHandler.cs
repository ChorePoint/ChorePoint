using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Application.Interfaces.Hangfire;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;

using Hangfire;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ChorePoint.Application.Handlers.Auth.AddKidLoginCode;

public class AddKidLoginCodeHandler(IAppDbContext context, IParentContextService parentContextService,
    IKidLoginCodeGenerator kidLoginCodeGenerator, IPasswordHasher<string> passwordHasher, IOptionsSnapshot<ApiOptions> apiOptions)
    : IRequestHandler<AddKidLoginCodeCommand, AddKidLoginCodeResponse>
{
    private readonly ApiOptions _apiOptions = apiOptions.Value;

    public async Task<AddKidLoginCodeResponse> Handle(AddKidLoginCodeCommand request, CancellationToken cancellationToken)
    {
        var kid = await context.Kids.FindAsync([request.KidId], cancellationToken);

        if (kid is null)
        {
            throw new NotFoundException($"No kid exists with ID [{request.KidId}]");
        }

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(kid.ParentId, parentId);

        var loginCodeString = kidLoginCodeGenerator.GenerateLoginCode();
        var loginCode = LoginCode.Create(kid.KidId, passwordHasher.HashPassword(string.Empty, loginCodeString));

        await context.LoginCodes.AddAsync(loginCode, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        BackgroundJob.Schedule<ILoginCodeDeletionJob>(
            j => j.StartDeleteJob(kid.KidId, cancellationToken),
            TimeSpan.FromMinutes(_apiOptions.KidLoginCodeTimeout)
        );

        return new AddKidLoginCodeResponse(loginCodeString);
    }
}
