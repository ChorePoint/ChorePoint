using MediatR;

namespace ChorePoint.Application.Handlers.Kid.GetKidSelf;

public record GetKidSelfQuery : IRequest<GetKidSelfResponse>;
