using ChorePoint.Application.Handlers.ChoreSubmission.CompleteChore;
using ChorePoint.Application.Handlers.ChoreSubmission.GetLatestSubmissionByKid;
using ChorePoint.Application.Handlers.ChoreSubmission.GetStatsByKid;
using ChorePoint.Application.Handlers.ChoreSubmission.GetSubmissionsByParent;
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
    [HttpGet("latest/{kidId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLatestSubmissionByKid(int kidId)
    {
        var result = await mediator.Send(new GetLatestSubmissionByKidQuery(kidId));
        return Ok(new
        {
            success = true,
            message = $"Latest chore submission with kid ID [{kidId}] successfully retrieved",
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
    public async Task<IActionResult> GetStatsByKid(int kidId)
    {
        var result = await mediator.Send(new GetStatsByKidQuery(kidId));
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
    public async Task<IActionResult> GetSubmissionsByParent([FromQuery] bool pending = false)
    {
        var result = await mediator.Send(new GetSubmissionsByParentQuery(pending));
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
    public async Task<IActionResult> ReviewSubmission([FromBody] ReviewSubmissionCommand command)
    {
        await mediator.Send(command);
        return Ok(new
        {
            success = true,
            message = $"Chore submission with ID [{command.ChoreSubmissionId}] reviewed successfully"
        });
    }
}