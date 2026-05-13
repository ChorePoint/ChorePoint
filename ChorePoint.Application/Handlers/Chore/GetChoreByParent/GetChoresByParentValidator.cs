using FluentValidation;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByUser;

public class GetChoresByParentValidator : AbstractValidator<GetChoresByUserQuery>
{
    public GetChoresByParentValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}