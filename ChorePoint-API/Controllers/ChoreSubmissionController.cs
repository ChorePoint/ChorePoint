using ChorePoint_API.Models;
using ChorePoint_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint_API.Controllers
{
    [ApiController]
    [Route("api/Chore")]
    public class ChoreSubmissionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ChoreSubmissionService _service;

        public ChoreSubmissionController(AppDbContext context, ChoreSubmissionService service)
        {
            _context = context;
            _service = service;
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> CompleteChore(int id)
        {
            var result = await _service.CompleteChore(id);
            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            return Ok(new { message = "Chore marked as completed.", completionId = result.Code });
        }

        [HttpGet("current/{userId}")]
        public async Task<ActionResult<ChoreSubmission>> GetCurrent(int userId)
        {
            var choreCompletions = await _context.ChoreCompletions.Where(c => c.UserId == userId).ToListAsync();
            if (choreCompletions == null || choreCompletions.Count == 0)
                return NotFound();

            var choreCompletion = choreCompletions
                .OrderByDescending(x => x.CompletedAt)
                .FirstOrDefault();

            return choreCompletion;
        }

        // GET: api/chore/stats?kidId=1
        [Authorize]
        [HttpGet("stats/{kidId}")]
        public async Task<ActionResult<KidStatsDto>> GetKidStats(int kidId)
        {
            var stats = await _service.GetChoreCompletionStatsByKidId(kidId);
            if (stats == null)
                return NotFound();

            return Ok(stats);
        }
    }
}
