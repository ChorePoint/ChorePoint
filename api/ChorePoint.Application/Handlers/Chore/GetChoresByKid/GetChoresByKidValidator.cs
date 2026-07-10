using FluentValidation;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByKid;

public class GetChoresByKidValidator : AbstractValidator<GetChoresByKidQuery>
{
    public GetChoresByKidValidator()
    {
        RuleFor(x => x.KidId).NotEmpty().WithMessage("KidId is required");
    }
}
