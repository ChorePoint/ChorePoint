using MediatR;

namespace ChorePoint.Application.RequestHandlers.Parent.UpdateKid;

public record UpdateKidCommand(
    int KidId,
    string Name,
    string Avatar,
    int? Age,
    int LifetimePoints,
    int SpendablePoints
) : IRequest;
