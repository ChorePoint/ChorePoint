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

        // Convert List<DayOfWeek> to a comma-separated string for storage
        modelBuilder.Entity<ParentSettings>(entity =>
        {
            entity.Property(e => e.ShopOpeningDays)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<DayOfWeek>>(v, (JsonSerializerOptions?)null)!);
        });
    }
}