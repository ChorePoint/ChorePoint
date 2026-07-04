using FluentValidation;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetSubmissionsByParent;

public class GetSubmissionsByParentValidator : AbstractValidator<GetSubmissionsByParentQuery>
{
    public GetSubmissionsByParentValidator()
    {
        RuleFor(x => x.Pending)
            .NotNull().WithMessage("Pending is required");
    }
}