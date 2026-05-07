using ChorePoint.Application.Handlers.ChoreSubmission.CompleteChore;
using ChorePoint.Application.Handlers.ChoreSubmission.GetCurrent;
using ChorePoint.Application.Handlers.ChoreSubmission.GetKidsStats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChorePoint.API.Controllers;

[ApiController]
[Route("api/chore/submissions")]
public class ChoreSubmissionController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChoreSubmissionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{id:int}/complete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CompleteChore(int id)
    {
        await _mediator.Send(new CompleteChoreCommand(id));
        return Ok(new
        {
            success = true,
            message = "Chore completed successfully"
        });
    }

    [HttpGet("current/{userId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrent(int userId)
    {
        var result = await _mediator.Send(new GetCurrentQuery(userId));
        return Ok(new
        {
            success = true,
            message = "Current chore successfully retrieved",
            data = result
        });
    }

    [Authorize]
    [HttpGet("stats/{kidId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetKidStats(int kidId)
    {
        var result = await _mediator.Send(new GetKidsStatsQuery(kidId));
        return Ok(new
        {
            success = true,
            message = "Kid's stats retrieved successfully",
            data = result
        });
    }
}