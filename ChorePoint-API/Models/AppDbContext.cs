using Microsoft.EntityFrameworkCore;

namespace ChorePoint_API.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Parent> Parents => Set<Parent>();
        public DbSet<Chore> Chores => Set<Chore>();
        public DbSet<ChoreSubmission> ChoreCompletions => Set<ChoreSubmission>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chore>(entity =>
            {
                entity.Property(e => e.Difficulty)
                      .HasConversion<string>()
                      .HasMaxLength(10);

                entity.Property(e => e.Frequency)
                      .HasConversion<string>()
                      .HasMaxLength(10);
            });

            modelBuilder.Entity<ChoreSubmission>(entity =>
            {
                entity.Property(e => e.ApprovalStatus)
                        .HasConversion<string>()
                        .HasMaxLength(10);
            });
        }
    }
}
