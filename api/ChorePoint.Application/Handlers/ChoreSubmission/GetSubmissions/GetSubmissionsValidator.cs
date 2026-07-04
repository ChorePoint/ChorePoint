using FluentValidation;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetSubmissions;

public class GetSubmissionsValidator : AbstractValidator<GetSubmissionsQuery>
{
    public GetSubmissionsValidator()
    {
        RuleFor(x => x.Pending)
            .NotNull().WithMessage("Pending is required");
    }
}