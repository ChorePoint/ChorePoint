using FluentValidation;

namespace ChorePoint.Application.Handlers.Shop.ReactivateShopItem;

public class ReactivateShopItemValidator : AbstractValidator<ReactivateShopItemCommand>
{
    public ReactivateShopItemValidator()
    {
        RuleFor(x => x.ShopItemId).NotEmpty().WithMessage("ShopItemId is required");
    }
}
