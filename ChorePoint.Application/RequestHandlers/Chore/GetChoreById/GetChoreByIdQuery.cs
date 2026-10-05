using MediatR;

namespace ChorePoint.Application.RequestHandlers.Chore.GetChoreById;

public record GetChoreByIdQuery(int ChoreId) : IRequest<GetChoreByIdResponse>;
