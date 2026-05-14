using FluentValidation;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByUser;

public class GetChoresByUserValidator : AbstractValidator<GetChoresByUserQuery>
{
    public GetChoresByUserValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required");
    }
}