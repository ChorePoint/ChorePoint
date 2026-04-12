using ChorePoint_API.Models.Requests;
using ChorePoint_API.Models.Requsts;
using ChorePoint_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChorePoint_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly TokenService _tokenService;

        public AuthController(AuthService authService, TokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var success = await _authService.Register(request.FirstName, request.LastName, request.Email, request.Password);

            if (!success)
                return BadRequest("A user with this email already exists!");

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var parent = await _authService.Login(request.Email, request.Password);

            if (parent == null)
                return Unauthorized("Invalid email or password");

            var token = _tokenService.CreateToken(parent);

            return Ok(new { token });
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
