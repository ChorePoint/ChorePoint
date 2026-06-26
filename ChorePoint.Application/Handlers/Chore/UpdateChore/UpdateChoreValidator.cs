using FluentValidation;

namespace ChorePoint.Application.Handlers.Chore.UpdateChore;

public class UpdateChoreValidator : AbstractValidator<UpdateChoreCommand>
{
    public UpdateChoreValidator()
    {
        RuleFor(x => x.ChoreId)
            .NotEmpty().WithMessage("ChoreId is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Icon)
            .NotEmpty().WithMessage("Icon is required");

        RuleFor(x => x.Points)
            .NotEmpty().WithMessage("Points is required");

        RuleFor(x => x.Difficulty)
            .NotEmpty().WithMessage("Difficulty is required");

        RuleFor(x => x.Frequency)
            .NotEmpty().WithMessage("Frequency is required");

        RuleForEach(x => x.AssignedKids)
            .NotEmpty().WithMessage("AssignedKids is required")
            .ChildRules(assignedKid =>
            {
                assignedKid.RuleFor(x => x.KidId)
                    .NotEmpty().WithMessage("KidId is required in AssignedKids");

                assignedKid.RuleFor(x => x.IsVisible)
                    .NotEmpty().WithMessage("IsVisible is required in AssignedKids");
            });
    }
}