namespace ChorePoint.Application.RequestHandlers.Parent.GetKidsByParent;

public record GetKidsByParentResponse(
    int KidId,
    string Name,
    string Avatar,
    int? Age,
    int LifetimePoints,
    int SpendablePoints
);
