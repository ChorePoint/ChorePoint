using MediatR;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetLatestSubmissionByKid;

public record GetLatestSubmissionByKidQuery(
    int KidId
) : IRequest<GetLatestSubmissionByKidResponse>;