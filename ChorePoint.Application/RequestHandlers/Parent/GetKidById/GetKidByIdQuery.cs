using MediatR;

namespace ChorePoint.Application.RequestHandlers.Parent.GetKidById;

public record GetKidByIdQuery(int KidId) : IRequest<GetKidByIdResponse>;
