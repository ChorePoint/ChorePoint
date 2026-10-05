namespace ChorePoint.Application.RequestHandlers.Parent.GetKidById;

public record GetKidByIdResponse(
    int KidId,
    string Name,
    string Avatar,
    int? Age,
    int LifetimePoints,
    int SpendablePoints
);
