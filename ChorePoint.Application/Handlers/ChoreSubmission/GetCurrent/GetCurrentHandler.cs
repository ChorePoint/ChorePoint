using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetCurrent;

public class GetCurrentHandler : IRequestHandler<GetCurrentQuery, GetCurrentResponse>
{
    private readonly IAppDbContext _context;

    public GetCurrentHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<GetCurrentResponse> Handle(GetCurrentQuery request, CancellationToken cancellationToken)
    {
        var currentCompletion = await _context.ChoreSubmissions
            .Where(c => c.UserId == request.Id)
            .OrderByDescending(c => c.CompletedAt)
            .ProjectToType<GetCurrentResponse>()
            .FirstOrDefaultAsync(cancellationToken);

        return currentCompletion
               ?? throw new NotFoundException($"No completed chores exist for user id: {request.Id}");
    }
}