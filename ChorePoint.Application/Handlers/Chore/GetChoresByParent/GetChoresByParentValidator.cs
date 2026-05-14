using ChorePoint.Application.Handlers.Chore.GetChoresByUser;
using FluentValidation;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByParent;

public class GetChoresByParentValidator : AbstractValidator<GetChoresByUserQuery>
{
    public GetChoresByParentValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}