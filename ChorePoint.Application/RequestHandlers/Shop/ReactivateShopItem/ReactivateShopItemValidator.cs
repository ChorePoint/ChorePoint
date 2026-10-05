using FluentValidation;

namespace ChorePoint.Application.RequestHandlers.Shop.ReactivateShopItem;

public class ReactivateShopItemValidator : AbstractValidator<ReactivateShopItemCommand>
{
    public ReactivateShopItemValidator()
    {
        RuleFor(x => x.ShopItemId).NotEmpty().WithMessage("ShopItemId is required");
    }
}
