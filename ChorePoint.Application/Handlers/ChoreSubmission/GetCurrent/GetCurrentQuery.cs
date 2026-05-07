using MediatR;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetCurrent;

public record GetCurrentQuery(
    int Id
) : IRequest<GetCurrentResponse>;