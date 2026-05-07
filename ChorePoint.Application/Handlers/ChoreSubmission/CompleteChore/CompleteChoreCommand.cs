using MediatR;

namespace ChorePoint.Application.Handlers.ChoreSubmission.CompleteChore;

public record CompleteChoreCommand(
    int Id
) : IRequest;