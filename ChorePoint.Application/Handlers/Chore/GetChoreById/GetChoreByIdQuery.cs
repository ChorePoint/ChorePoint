using MediatR;

namespace ChorePoint.Application.Handlers.Chore.GetChoreById;

public record GetChoreByIdQuery(int ChoreId) : IRequest<GetChoreByIdResponse>;
