using ChorePoint.Application.Handlers.Chore.Create;
using ChorePoint.Application.Handlers.Chore.GetChoreById;
using ChorePoint.Application.Handlers.Chore.GetChoresByKid;
using ChorePoint.Application.Handlers.Chore.GetChoresByParent;
using ChorePoint.Application.Handlers.Chore.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChorePoint.API.Controllers;

[ApiController]
[Route("api/chore")]
public class ChoreController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpGet("{choreId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetChoreById(int choreId)
    {
        var result = await mediator.Send(new GetChoreByIdQuery(choreId));
        return Ok(new
        {
            success = true,
            message = $"Chore with ID [{choreId}] successfully retrieved",
            data = result
        });
    }

    [Authorize]
    [HttpGet("kid/{kidId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetChoresByKid(int kidId)
    {
        var result = await mediator.Send(new GetChoresByKidQuery(kidId));
        return Ok(new
        {
            success = true,
            message = $"Chores with kid ID [{kidId}] successfully retrieved",
            data = result
        });
    }

    [Authorize]
    [HttpGet("parent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetChoresByParent([FromQuery] bool? visible)
    {
        var result = await mediator.Send(new GetChoresByParentQuery(visible));
        return Ok(new
        {
            success = true,
            message = "Chores successfully retrieved",
            data = result
        });
    }

    [Authorize]
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateChoreCommand command)
    {
        await mediator.Send(command);
        return Ok(new
        {
            success = true,
            message = $"Chore with name [{command.Name}] successfully created"
        });
    }

    [Authorize]
    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromBody] UpdateChoreCommand command)
    {
        await mediator.Send(command);
        return Ok(new
        {
            success = true,
            message = $"Chore with name [{command.Name}] successfully updated"
        });
    }
}