using FluentValidation;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetCurrent;

public class GetCurrentValidator : AbstractValidator<GetCurrentQuery>
{
    public GetCurrentValidator()
    {
        RuleFor(x => x.KidId)
            .NotEmpty().WithMessage("KidId is required");
    }
}