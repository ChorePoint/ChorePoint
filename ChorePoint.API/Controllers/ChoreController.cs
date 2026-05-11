using ChorePoint.Application.Handlers.Chore.Create;
using ChorePoint.Application.Handlers.Chore.GetChoreById;
using ChorePoint.Application.Handlers.Chore.GetChoresByUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChorePoint.API.Controllers;

[ApiController]
[Route("api/chore")]
public class ChoreController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetChoreById(int id)
    {
        var result = await mediator.Send(new GetChoreByIdQuery(id));
        return Ok(new
        {
            success = true,
            message = $"Chore with id {id} successfully retrieved",
            data = result
        });
    }

    [HttpGet("user/{userId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetChoresByUser(int userId)
    {
        var result = await mediator.Send(new GetChoresByUserQuery(userId));
        return Ok(new
        {
            success = true,
            message = $"Chores from user id {userId} successfully retrieved",
            data = result
        });
    }

    [Authorize]
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateChoreCommand command)
    {
        await mediator.Send(command);
        return Ok(new
        {
            success = true,
            message = $"Chore with name {command.Name} successfully created"
        });
    }
}