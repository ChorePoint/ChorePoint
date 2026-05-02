using ChorePoint.API.Models.Requests;
using ChorePoint.API.Services;
using ChorePoint.Application.Handlers.Auth.Login;
using ChorePoint.Domain.Entities.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChorePoint.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var authResult = await _authService.Register(request.FirstName, request.LastName, request.Email, request.Password);

            if (!authResult.Success)
                return BadRequest(authResult.ErrorMessage);

            return Ok();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new
            {
                success = true,
                message = "Login successful",
                data = result
            });
        }

        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAccount(RegisterRequest request)
        {
            var parent = await _authService.Register(request.FirstName, request.LastName, request.Email, request.Password);

            if (parent == null)
                return Unauthorized("Invalid email or password");

            //var token = _tokenService.CreateToken(parent);

            return Ok();
        }
    }
}
