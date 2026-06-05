using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChorePoint.API.Controllers;

[ApiController]
[Route("api/shop")]
public class ShopController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetShopItems()
    {
        var result = await mediator.Send(new GetShopItemsCommand());
        return Ok(new
        {
            success = true,
            message = "Shop items retrieved successfully",
            data = result
        });
    }
    
    [Authorize]
    [HttpPost("new")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> NewShopItem([FromBody] NewShopItemCommand command)
    {
        await mediator.Send(command);
        return Ok(new
        {
            success = true,
            message = "New shop item created successfully",
        });
    }
    
    [Authorize]
    [HttpPost("buy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> BuyShopItem([FromBody] BuyShopItemCommand command)
    {
        await mediator.Send(command);
        return Ok(new
        {
            success = true,
            message = $"Shop item with ID [{command.ShopItemId}] bought successfully",
        });
    }
    
    [Authorize]
    [HttpPost("review")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> BuyShopItem([FromBody] ReviewShopItemCommand command)
    {
        await mediator.Send(command);
        return Ok(new
        {
            success = true,
            message = $"Shop item with ID [{command.ShopItemId}] bought successfully",
        });
    }
    
    [Authorize]
    [HttpPost("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateShopItem([FromBody] UpdateShopItemCommand command)
    {
        await mediator.Send(command);
        return Ok(new
        {
            success = true,
            message = $"Shop item with ID [{command.ShopItemId}] updated successfully",
        });
    }
}