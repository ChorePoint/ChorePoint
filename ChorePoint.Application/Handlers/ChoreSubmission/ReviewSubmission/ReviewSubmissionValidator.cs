using FluentValidation;

namespace ChorePoint.Application.Handlers.ChoreSubmission.ReviewSubmission;

public class ReviewSubmissionValidator : AbstractValidator<ReviewSubmissionCommand>
{
    public ReviewSubmissionValidator()
    {
        RuleFor(x => x.ChoreSubmissionId)
            .NotEmpty().WithMessage("ChoreSubmissionId is required");

        RuleFor(x => x.Approve)
            .NotNull().WithMessage("Approve is required");
    }
}