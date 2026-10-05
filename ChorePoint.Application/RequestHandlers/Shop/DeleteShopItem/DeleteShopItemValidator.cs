using FluentValidation;

namespace ChorePoint.Application.RequestHandlers.Shop.DeleteShopItem;

public class DeleteShopItemValidator : AbstractValidator<DeleteShopItemCommand>
{
    public DeleteShopItemValidator()
    {
        RuleFor(x => x.ShopItemId).NotEmpty().WithMessage("ShopItemId is required");
    }
}
