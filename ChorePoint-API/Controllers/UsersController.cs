using ChorePoint_API.Models;
using ChorePoint_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ChorePoint_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/users/me
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<User>> GetUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userService.GetUserById(int.Parse(userId));

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // GET: api/users/kids
        [Authorize]
        [HttpGet("kids")]
        public async Task<ActionResult<User>> GetKids()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var kids = await _userService.GetKidsByParentId(int.Parse(userId));

            return Ok(kids);
        }
    }
}
