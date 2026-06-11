using FluentValidation;

namespace ChorePoint.Application.Handlers.Shop.NewShopItem;

public class NewShopItemValidator : AbstractValidator<NewShopItemCommand>
{
    public NewShopItemValidator()
    {
        RuleFor(x => x.KidId)
            .NotEmpty().WithMessage("KidId is required");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Cost)
            .NotEmpty().WithMessage("Cost is required");

        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("Quantity is required");
    }
}