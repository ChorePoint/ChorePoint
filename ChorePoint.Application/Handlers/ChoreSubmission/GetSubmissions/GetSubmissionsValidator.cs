using FluentValidation;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetSubmissions;

public class GetSubmissionsValidator : AbstractValidator<GetSubmissionsQuery>
{
    public GetSubmissionsValidator()
    {
        RuleFor(x => x.Pending)
            .NotEmpty().WithMessage("Pending is required");
    }
}