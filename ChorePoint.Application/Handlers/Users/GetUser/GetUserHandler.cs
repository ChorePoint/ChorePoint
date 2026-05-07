using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;

namespace ChorePoint.Application.Handlers.Users.GetUser;

public class GetUserHandler : IRequestHandler<GetUserQuery, GetUserResponse>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _userService;

    public GetUserHandler(IAppDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetUserId();
        if (userId == null)
            throw new UnauthorizedAccessException("User not authorised");

        var user = await _context.Users
            .FindAsync(userId, cancellationToken);

        return user.Adapt<GetUserResponse>()
               ?? throw new NotFoundException($"No user exists with id: {userId}");
    }
}