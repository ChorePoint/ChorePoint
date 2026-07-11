using ChorePoint.Application.Handlers.Shop.BuyShopItem;
using ChorePoint.Application.Handlers.Shop.DeleteShopItem;
using ChorePoint.Application.Handlers.Shop.GetShopItemsByKid;
using ChorePoint.Application.Handlers.Shop.GetShopItemsByParent;
using ChorePoint.Application.Handlers.Shop.NewShopItem;
using ChorePoint.Application.Handlers.Shop.ReactivateShopItem;
using ChorePoint.Application.Handlers.Shop.ReviewShopItemPurchase;
using ChorePoint.Application.Handlers.Shop.UpdateShopItem;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChorePoint.API.Controllers;

[ApiController]
[Route("api/shop")]
public class ShopController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpPost("buy/{shopItemId:int}/{kidId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> BuyShopItem(int shopItemId, int kidId)
    {
        await mediator.Send(new BuyShopItemCommand(shopItemId, kidId));
        return Ok(
            new
            {
                success = true,
                message = $"Shop item with ID [{shopItemId}] bought successfully by kid with ID [{kidId}]",
            }
        );
    }

    [Authorize]
    [HttpDelete("delete/{shopItemId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteShopItem(int shopItemId)
    {
        await mediator.Send(new DeleteShopItemCommand(shopItemId));
        return Ok(
            new
            {
                success = true,
                message = $"Shop item with ID [{shopItemId}] deleted successfully",
            }
        );
    }

    [Authorize]
    [HttpGet("kid/{kidId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetShopItemsByKid(int kidId)
    {
        var result = await mediator.Send(new GetShopItemsByKidQuery(kidId));
        return Ok(
            new
            {
                success = true,
                message = $"Shop items for kid with ID [{kidId}] retrieved successfully",
                data = result,
            }
        );
    }

    [Authorize]
    [HttpGet("parent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetShopItemsByParent()
    {
        var result = await mediator.Send(new GetShopItemsByParentQuery());
        return Ok(
            new
            {
                success = true,
                message = "Shop items retrieved successfully",
                data = result,
            }
        );
    }

    [Authorize]
    [HttpPost("new")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> NewShopItem([FromBody] NewShopItemCommand command)
    {
        await mediator.Send(command);
        return Ok(
            new
            {
                success = true,
                message = $"Shop item with name [{command.Name}] created successfully",
            }
        );
    }

    [Authorize]
    [HttpPost("reactivate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ReactivateShopItem(
        [FromBody] ReactivateShopItemCommand command
    )
    {
        await mediator.Send(command);
        return Ok(
            new
            {
                success = true,
                message = $"Shop item with ID [{command.ShopItemId}] reactivated successfully",
            }
        );
    }

    [Authorize]
    [HttpPost("review")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ReviewShopItemPurchase(
        [FromBody] ReviewShopItemPurchaseCommand command
    )
    {
        await mediator.Send(command);
        return Ok(
            new
            {
                success = true,
                message = $"Purchase of shop item with ID [{command.ShopItemId}] reviewed successfully",
            }
        );
    }

    [Authorize]
    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateShopItem([FromBody] UpdateShopItemCommand command)
    {
        await mediator.Send(command);
        return Ok(
            new
            {
                success = true,
                message = $"Shop item with ID [{command.ShopItemId}] updated successfully",
            }
        );
    }
}
