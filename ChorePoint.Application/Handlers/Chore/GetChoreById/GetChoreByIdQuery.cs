using MediatR;

namespace ChorePoint.Application.Handlers.Chore.GetChoreById;

public record GetChoreByIdQuery(
    int Id
) : IRequest<GetChoreByIdResponse>;