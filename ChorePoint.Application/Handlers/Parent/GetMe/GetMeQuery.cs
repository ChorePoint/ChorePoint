using MediatR;

namespace ChorePoint.Application.Handlers.Parent.GetMe;

public record GetMeQuery : IRequest<GetMeResponse>;