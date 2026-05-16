using FluentValidation;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetKidsStats;

public class GetSubmissionsValidator : AbstractValidator<GetKidsStatsQuery>
{
    public GetSubmissionsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required");
    }
}