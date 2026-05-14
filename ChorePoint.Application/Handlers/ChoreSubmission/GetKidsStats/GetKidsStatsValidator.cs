using FluentValidation;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetKidsStats;

public class GetKidsStatsValidator : AbstractValidator<GetKidsStatsQuery>
{
    public GetKidsStatsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required");
    }
}