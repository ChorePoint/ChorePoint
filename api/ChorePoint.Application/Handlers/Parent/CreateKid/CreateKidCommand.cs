using MediatR;

namespace ChorePoint.Application.Handlers.Parent.CreateKid;

public record CreateKidCommand(
    string Name,
    string Avatar,
    int? Age
) : IRequest;