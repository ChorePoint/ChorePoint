using FluentValidation;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetCurrent;

public class GetCurrentValidator : AbstractValidator<GetCurrentQuery>
{
    public GetCurrentValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}