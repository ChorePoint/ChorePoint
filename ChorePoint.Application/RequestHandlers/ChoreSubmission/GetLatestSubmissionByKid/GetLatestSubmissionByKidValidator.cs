using FluentValidation;

namespace ChorePoint.Application.RequestHandlers.ChoreSubmission.GetLatestSubmissionByKid;

public class GetLatestSubmissionByKidValidator : AbstractValidator<GetLatestSubmissionByKidQuery>
{
    public GetLatestSubmissionByKidValidator()
    {
        RuleFor(x => x.KidId).NotEmpty().WithMessage("KidId is required");
    }
}
