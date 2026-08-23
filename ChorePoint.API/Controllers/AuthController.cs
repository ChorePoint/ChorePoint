using ChorePoint.Application.Handlers.Auth.AddKidLoginCode;
using ChorePoint.Application.Handlers.Auth.KidLogin;
using ChorePoint.Application.Handlers.Auth.ParentLogin;
using ChorePoint.Application.Handlers.Auth.Register;
using ChorePoint.Infrastructure.Authentication;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChorePoint.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("code/add")]
    [Authorize(Roles = JwtConstants.ParentRole)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddKidLoginCode([FromBody] AddKidLoginCodeCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(
            new
            {
                success = true,
                message = $"Login code added to kid with ID [{command.KidId}] successfully",
                data = result
            }
        );
    }

    [HttpPost("login/kid")]
    [AllowAnonymous]
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
    [AllowAnonymous]
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
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        await mediator.Send(command);
        return Ok(new { success = true, message = "Parent registered successfully" });
    }
}
