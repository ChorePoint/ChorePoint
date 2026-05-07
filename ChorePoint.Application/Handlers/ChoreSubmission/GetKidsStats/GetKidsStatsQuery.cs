using MediatR;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetKidsStats;

public record GetKidsStatsQuery(
    int Id
) : IRequest<GetKidsStatsResponse>;