using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetKidsStats;

public class GetKidsStatsHandler : IRequestHandler<GetKidsStatsQuery, GetKidsStatsResponse>
{
    private readonly IAppDbContext _context;
    
    public GetKidsStatsHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<GetKidsStatsResponse> Handle(GetKidsStatsQuery request, CancellationToken cancellationToken)
    {
        var choreSubmissions = await _context.ChoreSubmissions
            .Where(c => c.UserId == request.Id)
            .ToListAsync(cancellationToken);
        
        if (choreSubmissions.Count == 0)
            throw new NotFoundException($"No chore submissions found for user id: {request.Id}");
        
        var startOfWeek = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);
        return new GetKidsStatsResponse
        (
            choreSubmissions.Count(cs => cs.CompletedThisWeek(startOfWeek)),
            choreSubmissions.Count == 0 ? 0 :
                (int)(choreSubmissions.Count(cs => cs.ApprovalStatus == ChoreApprovalStatus.Approved) * 100.0 / choreSubmissions.Count)
        );
    }
}