using FluentValidation;

namespace ChorePoint.Application.Handlers.Chore.UpdateChore;

public class UpdateChoreValidator : AbstractValidator<UpdateChoreCommand>
{
    public UpdateChoreValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID is required");
        
        RuleFor(x => x.KidId)
            .NotEmpty().WithMessage("KidId is required");

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

        RuleFor(x => x.IsVisible)
            .NotEmpty().WithMessage("IsVisible is required");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required");

        RuleFor(x => x.DueDay)
            .NotEmpty().WithMessage("DueDay is required");
    }
}