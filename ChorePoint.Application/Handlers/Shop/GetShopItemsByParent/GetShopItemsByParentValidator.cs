using ChorePoint.Application.Handlers.Chore.GetChoresByParent;
using FluentValidation;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByParent;

public class GetShopItemsByParentValidator : AbstractValidator<GetChoresByParentQuery>
{
    public GetShopItemsByParentValidator()
    {
        RuleFor(x => x.IsVisible)
            .NotEmpty().WithMessage("IsVisible is required");
    }
}