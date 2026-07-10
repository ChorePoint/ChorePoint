using ChorePoint.Application.Handlers.Parent.CreateKid;
using ChorePoint.Application.Handlers.Parent.DeleteKid;
using ChorePoint.Application.Handlers.Parent.GetKidById;
using ChorePoint.Application.Handlers.Parent.GetKidsByParent;
using ChorePoint.Application.Handlers.Parent.UpdateKid;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChorePoint.API.Controllers;

[ApiController]
[Route("api/parent")]
public class ParentController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpPost("kid/create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateKid([FromBody] CreateKidCommand command)
    {
        await mediator.Send(command);
        return Ok(
            new { success = true, message = $"Kid with name [{command.Name}] successfully created" }
        );
    }

    [Authorize]
    [HttpDelete("kid/delete/{kidId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteKid(int kidId)
    {
        await mediator.Send(new DeleteKidCommand(kidId));
        return Ok(new { success = true, message = $"Kid with ID [{kidId}] successfully deleted" });
    }

    [Authorize]
    [HttpGet("kid/{kidId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetKidById(int kidId)
    {
        var result = await mediator.Send(new GetKidByIdQuery(kidId));
        return Ok(
            new
            {
                success = true,
                message = $"Kid details with ID [{kidId}] retrieved successfully",
                data = result,
            }
        );
    }

    [Authorize]
    [HttpGet("kids")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetKidsByParent()
    {
        var result = await mediator.Send(new GetKidsByParentQuery());
        return Ok(
            new
            {
                success = true,
                message = "All kid's details retrieved successfully",
                data = result,
            }
        );
    }

    [Authorize]
    [HttpPut("kid/update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateKid([FromBody] UpdateKidCommand command)
    {
        await mediator.Send(command);
        return Ok(
            new { success = true, message = $"Kid with ID [{command.KidId}] successfully updated" }
        );
    }
}
