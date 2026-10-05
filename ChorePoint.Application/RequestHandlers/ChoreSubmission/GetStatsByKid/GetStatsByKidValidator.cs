using FluentValidation;

namespace ChorePoint.Application.RequestHandlers.ChoreSubmission.GetStatsByKid;

public class GetStatsByKidValidator : AbstractValidator<GetStatsByKidQuery>
{
    public GetStatsByKidValidator()
    {
        RuleFor(x => x.KidId).NotEmpty().WithMessage("KidId is required");
    }
}
