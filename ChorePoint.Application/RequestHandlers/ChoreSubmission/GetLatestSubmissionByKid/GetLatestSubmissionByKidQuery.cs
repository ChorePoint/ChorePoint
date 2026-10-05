using MediatR;

namespace ChorePoint.Application.RequestHandlers.ChoreSubmission.GetLatestSubmissionByKid;

public record GetLatestSubmissionByKidQuery(int KidId) : IRequest<GetLatestSubmissionByKidResponse>;
