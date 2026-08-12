using ChorePoint.Application.Handlers.Auth.KidLogin;
using ChorePoint.Application.Handlers.Auth.ParentLogin;
using ChorePoint.Application.Handlers.Auth.Register;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChorePoint.API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login/kid")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> KidLogin([FromBody] KidLoginCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(
            new
            {
                success = true,
                message = "Kid login successful",
                data = result
            }
        );
    }

    [HttpPost("login/parent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ParentLogin([FromBody] ParentLoginCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(
            new
            {
                success = true,
                message = "Parent login successful",
                data = result
            }
        );
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        await mediator.Send(command);
        return Ok(new { success = true, message = "Parent registered successfully" });
    }
}
