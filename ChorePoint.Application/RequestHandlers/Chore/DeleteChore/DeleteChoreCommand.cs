using MediatR;

namespace ChorePoint.Application.RequestHandlers.Chore.DeleteChore;

public record DeleteChoreCommand(int ChoreId) : IRequest;
