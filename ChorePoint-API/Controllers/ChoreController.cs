using ChorePoint_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChoreController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChoreController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/chores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chore>> GetUser(int id)
        {
            var chore = await _context.Chores.FindAsync(id);
            if (chore == null)
                return NotFound();

            return chore;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Chore>>> GetChoresByUser(int userId)
        {
            var chores = await _context.Chores.Where(c => c.UserId == userId).ToListAsync();
            if (chores == null || chores.Count == 0)
                return NotFound();

            return chores;
        }
    }
}
