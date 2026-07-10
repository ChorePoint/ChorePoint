using FluentValidation;

namespace ChorePoint.Application.Handlers.Chore.GetChoreById;

public class GetChoreByIdValidator : AbstractValidator<GetChoreByIdQuery>
{
    public GetChoreByIdValidator()
    {
        RuleFor(x => x.ChoreId)
            .NotEmpty().WithMessage("ChoreId is required");
    }
}