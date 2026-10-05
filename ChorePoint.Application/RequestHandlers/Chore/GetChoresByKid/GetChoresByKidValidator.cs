using FluentValidation;

namespace ChorePoint.Application.RequestHandlers.Chore.GetChoresByKid;

public class GetChoresByKidValidator : AbstractValidator<GetChoresByKidQuery>
{
    public GetChoresByKidValidator()
    {
        RuleFor(x => x.KidId).NotEmpty().WithMessage("KidId is required");
    }
}
