using ChorePoint.Application.Handlers.ChoreSubmission.CompleteChore;
using ChorePoint.Application.Handlers.ChoreSubmission.GetCurrent;
using ChorePoint.Application.Handlers.ChoreSubmission.GetKidsStats;
using ChorePoint.Application.Handlers.ChoreSubmission.GetSubmissions;
using ChorePoint.Application.Handlers.ChoreSubmission.ReviewSubmission;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChorePoint.API.Controllers;

[ApiController]
[Route("api/chore/submissions")]
public class ChoreSubmissionController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetSubmissions([FromQuery] bool pending = false)
    {
        var result = await mediator.Send(new GetSubmissionsQuery(pending));
        return Ok(new
        {
            success = true,
            message = "Chore submissions retrieved successfully",
            data = result
        });
    }

    [Authorize]
    [HttpPost("{choreId:int}/complete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CompleteChore(int choreId)
    {
        await mediator.Send(new CompleteChoreCommand(choreId));
        return Ok(new
        {
            success = true,
            message = $"Chore with ID [{choreId}] completed successfully"
        });
    }

    [Authorize]
    [HttpGet("current/{kidId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCurrent(int kidId)
    {
        var result = await mediator.Send(new GetCurrentQuery(kidId));
        return Ok(new
        {
            success = true,
            message = $"Current chore submission with kid ID [{kidId}] successfully retrieved",
            data = result
        });
    }

    [Authorize]
    [HttpGet("stats/{kidId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetKidStats(int kidId)
    {
        var result = await mediator.Send(new GetKidsStatsQuery(kidId));
        return Ok(new
        {
            success = true,
            message = $"Kid with ID [{kidId}] stats retrieved successfully",
            data = result
        });
    }

    [Authorize]
    [HttpPost("{choreSubmissionId:int}/review")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ReviewChore(int choreSubmissionId, [FromQuery] bool approve = true)
    {
        await mediator.Send(new ReviewSubmissionCommand(choreSubmissionId, approve));
        return Ok(new
        {
            success = true,
            message = $"Chore with ID [{choreSubmissionId}] reviewed successfully"
        });
    }
}