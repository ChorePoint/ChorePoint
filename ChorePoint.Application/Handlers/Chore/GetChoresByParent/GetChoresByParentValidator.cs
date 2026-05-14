using FluentValidation;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByParent;

public class GetChoresByParentValidator : AbstractValidator<GetChoresByParentQuery>
{
    public GetChoresByParentValidator()
    {
        RuleFor(x => x.IsVisible)
            .NotEmpty().WithMessage("IsVisible is required");
    }
}