using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;

namespace ChorePoint.Domain.Entities
{
    [Table("chores")]
    public sealed class Chore
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(150)]
        [Column("name")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        [Column("icon")]
        public string Icon { get; set; } = "🎯";

        [Required]
        [Column("points")]
        public int Points { get; set; } = 0;

        [Required]
        [Column("difficulty")]
        public ChoreDifficulty Difficulty { get; set; }

        [Required]
        [Column("frequency")]
        public ChoreFrequency Frequency { get; set; } = ChoreFrequency.Daily;

        [Required]
        [MaxLength(50)]
        [Column("time_of_day")]
        public string TimeOfDay { get; set; } = null!;

        [Required]
        [Column("is_visible")]
        public bool IsVisible { get; set; } = true;

        [Column("last_completed_at")]
        public DateTime? LastCompletedAt { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("completion_count")]
        public int CompletionCount { get; set; } = 0;

        [Column("description")]
        public string? Description { get; set; }

        // Navigation property
        public User User { get; set; } = null!;


        public ChoreSubmission CreateSubmission(DateTime now)
        {
            return new ChoreSubmission
            {
                ChoreId = Id,
                UserId = UserId,
                CompletedAt = now,
                ApprovalStatus = ChoreApprovalStatus.Approved,
                ApprovedAt = now
            };
        }
        
        public void EnsureCanBeCompleted(ChoreSubmission? lastCompletion, DateTime now)
        {
            switch (Frequency)
            {
                case ChoreFrequency.Daily:
                    if (lastCompletion?.CompletedAt.Date == now.Date)
                        throw new ChoreAlreadyCompletedException("Chore already completed today");
                    break;
                case ChoreFrequency.Weekly:
                    if (lastCompletion?.CompletedAt.AddDays(7) > now)
                        throw new ChoreAlreadyCompletedException("Chore already completed within the last week");
                    break;
                case ChoreFrequency.Bonus:
                default:
                    throw new DomainException($"Unexpected frequency: {Frequency}");
            }
        }
    }
}
