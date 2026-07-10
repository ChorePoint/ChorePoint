using FluentValidation;

namespace ChorePoint.Application.Handlers.Shop.BuyShopItem;

public class BuyShopItemValidator : AbstractValidator<BuyShopItemCommand>
{
    public BuyShopItemValidator()
    {
        RuleFor(x => x.ShopItemId).NotEmpty().WithMessage("ShopItemId is required");

        RuleFor(x => x.KidId).NotEmpty().WithMessage("KidId is required");
    }
}
