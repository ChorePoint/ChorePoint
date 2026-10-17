using MediatR;

namespace ChorePoint.Application.Handlers.Kid.GetKidByKid;

public record GetKidByKidQuery: IRequest<GetKidByKidResponse>;
