using System.Text.Json;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Kid> Kids { get; set; } = null!;
    public DbSet<Parent> Parents { get; set; } = null!;
    public DbSet<Chore> Chores { get; set; } = null!;
    public DbSet<ChoreSubmission> ChoreSubmissions { get; set; } = null!;
    public DbSet<ParentSettings> ParentSettings { get; set; } = null!;
    public DbSet<ShopItem> ShopItems { get; set; } = null!;

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Here as an override if we want to add UpdatedAt columns on tables

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Chore>(entity =>
        {
            entity.Property(c => c.Difficulty)
                .HasConversion<string>()
                .HasMaxLength(10);

            entity.Property(c => c.Frequency)
                .HasConversion<string>()
                .HasMaxLength(10);
        });

        modelBuilder.Entity<ChoreSubmission>(entity =>
        {
            entity.Property(cs => cs.ApprovalStatus)
                .HasConversion<string>()
                .HasMaxLength(10);
        });

        // Convert List<DayOfWeek> to a comma-separated string for storage
        modelBuilder.Entity<ParentSettings>(entity =>
        {
            entity.Property(ps => ps.ShopOpeningDays)
                .HasConversion(
                    dow => JsonSerializer.Serialize(dow, (JsonSerializerOptions?)null),
                    dow => JsonSerializer.Deserialize<List<DayOfWeek>>(dow, (JsonSerializerOptions?)null)!);
        });

        modelBuilder.Entity<ShopItem>(entity =>
        {
            entity.Property(si => si.Status)
                .HasConversion<string>()
                .HasMaxLength(10);
        });
    }
}