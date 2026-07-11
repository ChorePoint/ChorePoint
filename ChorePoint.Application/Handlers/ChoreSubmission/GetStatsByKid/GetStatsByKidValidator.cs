using FluentValidation;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetStatsByKid;

public class GetStatsByKidValidator : AbstractValidator<GetStatsByKidQuery>
{
    public GetStatsByKidValidator()
    {
        RuleFor(x => x.KidId).NotEmpty().WithMessage("KidId is required");
    }
}
