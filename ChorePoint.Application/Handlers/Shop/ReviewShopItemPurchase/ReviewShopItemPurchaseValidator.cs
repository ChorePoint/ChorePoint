using FluentValidation;

namespace ChorePoint.Application.Handlers.Shop.ReviewShopItemPurchase;

public class ReviewShopItemPurchaseValidator : AbstractValidator<ReviewShopItemPurchaseCommand>
{
    public ReviewShopItemPurchaseValidator()
    {
        RuleFor(x => x.ShopItemId)
            .NotEmpty().WithMessage("ShopItemId is required");

        RuleFor(x => x.KidId)
            .NotEmpty().WithMessage("KidId is required");
    }
}