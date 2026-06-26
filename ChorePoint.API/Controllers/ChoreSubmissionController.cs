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
    [HttpPost("complete/{choreId:int}/{kidId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CompleteChore(int choreId, int kidId)
    {
        await mediator.Send(new CompleteChoreCommand(choreId, kidId));
        return Ok(new
        {
            success = true,
            message = $"Chore with ID [{choreId}] completed successfully by kid with ID [{kidId}]"
        });
    }
    
    [Authorize]
    [HttpGet("current/{kidId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
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
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
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
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
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
    [HttpPut("review")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ReviewChore([FromBody] ReviewSubmissionCommand command)
    {
        await mediator.Send(command);
        return Ok(new
        {
            success = true,
            message = $"Chore submission with ID [{command.ChoreSubmissionId}] reviewed successfully"
        });
    }
}