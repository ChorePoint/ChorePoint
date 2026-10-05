using MediatR;

namespace ChorePoint.Application.RequestHandlers.Auth.AddKidLoginCode;

public record AddKidLoginCodeCommand(int KidId) : IRequest<AddKidLoginCodeResponse>;
