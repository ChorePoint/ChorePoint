using ChorePoint.Application.Handlers.Chore.CreateChore;
using ChorePoint.Application.Handlers.Chore.DeleteChore;
using ChorePoint.Application.Handlers.Chore.GetChoreById;
using ChorePoint.Application.Handlers.Chore.GetChoresByKid;
using ChorePoint.Application.Handlers.Chore.GetChoresByParent;
using ChorePoint.Application.Handlers.Chore.UpdateChore;
using ChorePoint.Infrastructure.Authentication;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChorePoint.API.Controllers;

[Authorize]
[ApiController]
[Route("api/chore")]
public class ChoreController(IMediator mediator) : ControllerBase
{
    [HttpPost("create")]
    [Authorize(Roles = JwtConstants.ParentRole)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateChore([FromBody] CreateChoreCommand command)
    {
        await mediator.Send(command);
        return Ok(new { success = true, message = $"Chore with name [{command.Name}] successfully created" });
    }

    [HttpDelete("delete/{choreId:int}")]
    [Authorize(Roles = JwtConstants.ParentRole)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteChore(int choreId)
    {
        await mediator.Send(new DeleteChoreCommand(choreId));
        return Ok(new { success = true, message = $"Chore with ID [{choreId}] successfully deleted" });
    }

    [HttpGet("{choreId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetChoreById(int choreId)
    {
        var result = await mediator.Send(new GetChoreByIdQuery(choreId));
        return Ok(
            new
            {
                success = true,
                message = $"Chore with ID [{choreId}] successfully retrieved",
                data = result
            }
        );
    }

    [HttpGet("kid/{kidId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetChoresByKid(int kidId)
    {
        var result = await mediator.Send(new GetChoresByKidQuery(kidId));
        return Ok(
            new
            {
                success = true,
                message = $"Chores assigned to kid with ID [{kidId}] successfully retrieved",
                data = result
            }
        );
    }

    [HttpGet("parent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetChoresByParent([FromQuery] bool? isVisible)
    {
        var result = await mediator.Send(new GetChoresByParentQuery(isVisible));
        return Ok(
            new
            {
                success = true,
                message = "Chores successfully retrieved",
                data = result
            }
        );
    }

    [HttpPut("update")]
    [Authorize(Roles = JwtConstants.ParentRole)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateChore([FromBody] UpdateChoreCommand command)
    {
        await mediator.Send(command);
        return Ok(new { success = true, message = $"Chore with ID [{command.ChoreId}] successfully updated" });
    }
}
