using ChorePoint_API.Enums;
using ChorePoint_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint_API.Controllers
{
    [ApiController]
    [Route("api/Chore")]
    public class ChoreCompletionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChoreCompletionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> CompleteChore(uint id)
        {
            var chore = await _context.Chores
                .FirstOrDefaultAsync(c => c.Id == id && c.IsVisible);

            if (chore == null)
                return NotFound("Chore not found.");

            if (chore.Frequency == ChoreFrequency.Daily)
            {
                var today = DateTime.UtcNow.Date;

                bool alreadyCompletedToday = await _context.ChoreCompletions
                    .AnyAsync(c =>
                        c.ChoreId == id &&
                        c.CompletedAt >= today &&
                        c.CompletedAt < today.AddDays(1));

                if (alreadyCompletedToday)
                    return BadRequest("Chore already completed today.");
            }

            var completion = new ChoreCompletion
            {
                ChoreId = chore.Id,
                UserId = chore.UserId,
                CompletedAt = DateTime.UtcNow,
                ApprovalStatus = ChoreApprovalStatus.Pending
            };

            _context.ChoreCompletions.Add(completion);

            chore.LastCompletedAt = DateTime.UtcNow;
            chore.CompletionCount += 1;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Chore marked as completed.",
                completionId = completion.Id
            });
        }

        [HttpGet("current/{userId}")]
        public async Task<ActionResult<ChoreCompletion>> GetCurrent(int userId)
        {
            var choreCompletions = await _context.ChoreCompletions.Where(c => c.UserId == userId).ToListAsync();
            if (choreCompletions == null || choreCompletions.Count == 0)
                return NotFound();

            var choreCompletion = choreCompletions
                .OrderByDescending(x => x.CompletedAt)
                .FirstOrDefault();

            return choreCompletion;
        }
    }
}
