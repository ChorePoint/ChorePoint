using MediatR;

namespace ChorePoint.Application.RequestHandlers.ChoreSubmission.GetStatsByKid;

public record GetStatsByKidQuery(int KidId) : IRequest<GetStatsByKidResponse>;
