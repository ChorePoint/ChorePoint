using FluentValidation;

namespace ChorePoint.Application.Handlers.Shop.UpdateShopItem;

public class UpdateShopItemValidator : AbstractValidator<UpdateShopItemCommand>
{
    public UpdateShopItemValidator()
    {
        RuleFor(x => x.ShopItemId)
            .NotEmpty().WithMessage("ShopItemId is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Cost)
            .NotEmpty().WithMessage("Cost is required");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required");

        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("Quantity is required");
    }
}