using MediatR;

namespace ChorePoint.Application.RequestHandlers.ChoreSubmission.CompleteChore;

public record CompleteChoreCommand(int ChoreId, int KidId) : IRequest;
