using MediatR;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetStatsByKid;

public record GetStatsByKidQuery(int KidId) : IRequest<GetStatsByKidResponse>;
