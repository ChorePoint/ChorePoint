using ChorePoint.Application.Handlers.Chore.UpdateChore;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChorePoint.Application.Handlers.Parent.UpdateKid;

public class UpdateKidValidator : AbstractValidator<UpdateKidCommand>
{
    public UpdateKidValidator()
    {
        RuleFor(x => x.KidId)
            .NotEmpty().WithMessage("KidId is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
        
        RuleFor(x => x.Avatar)
            .NotEmpty().WithMessage("Avatar is required");
        
        RuleFor(x => x.DayStreak)
            .NotEmpty().WithMessage("DayStreak is required");
        
        RuleFor(x => x.LifetimePoints)
            .NotEmpty().WithMessage("LifetimePoints is required");

        RuleFor(x => x.SpendablePoints)
            .NotEmpty().WithMessage("SpendablePoints is required");

    }
}
