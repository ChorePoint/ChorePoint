using ChorePoint.Application.Handlers.Kid.GetKidSelf;
using ChorePoint.Infrastructure.Authentication;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChorePoint.API.Controllers;

[Authorize(Roles = AuthenticationConstants.KidRole)]
[ApiController]
[Route("api/kid")]
public class KidController(IMediator mediator) : ControllerBase
{
    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetKidSelf()
    {
        var result = await mediator.Send(new GetKidSelfQuery());
        return Ok(
            new
            {
                success = true,
                message = $"Kid details retrieved successfully",
                data = result
            }
        );
    }
}
