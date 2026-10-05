using FluentValidation;

namespace ChorePoint.Application.RequestHandlers.ChoreSubmission.CompleteChore;

public class CompleteChoreValidator : AbstractValidator<CompleteChoreCommand>
{
    public CompleteChoreValidator()
    {
        RuleFor(x => x.ChoreId).NotEmpty().WithMessage("ChoreId is required");

        RuleFor(x => x.KidId).NotEmpty().WithMessage("KidId is required");
    }
}
