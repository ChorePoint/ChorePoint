using MediatR;

namespace ChorePoint.Application.RequestHandlers.Parent.CreateKid;

public record CreateKidCommand(string Name, string Avatar, int? Age) : IRequest;
