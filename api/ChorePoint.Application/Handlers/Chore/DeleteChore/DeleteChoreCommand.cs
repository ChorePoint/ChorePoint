using MediatR;

namespace ChorePoint.Application.Handlers.Chore.DeleteChore;

public record DeleteChoreCommand(int ChoreId) : IRequest;
