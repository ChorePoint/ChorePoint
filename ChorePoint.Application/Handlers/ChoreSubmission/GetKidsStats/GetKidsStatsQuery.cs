using MediatR;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetKidsStats;

public record GetKidsStatsQuery(
    int UserId
) : IRequest<GetKidsStatsResponse>;