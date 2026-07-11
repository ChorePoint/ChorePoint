using MediatR;

namespace ChorePoint.Application.Handlers.Parent.GetKidById;

public record GetKidByIdQuery(int KidId) : IRequest<GetKidByIdResponse>;
