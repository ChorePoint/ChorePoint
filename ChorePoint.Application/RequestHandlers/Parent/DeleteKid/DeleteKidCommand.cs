using MediatR;

namespace ChorePoint.Application.RequestHandlers.Parent.DeleteKid;

public record DeleteKidCommand(int KidId) : IRequest;
