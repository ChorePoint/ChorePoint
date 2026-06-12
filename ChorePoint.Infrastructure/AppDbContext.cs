using System.Text.Json;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Chore> Chores { get; set; }

    public DbSet<ChoreSubmission> ChoreSubmissions { get; set; }

    public DbSet<Kid> Kids { get; set; }

    public DbSet<Parent> Parents { get; set; }

    public DbSet<ParentSettings> ParentSettings { get; set; }

    public DbSet<ShopItem> ShopItems { get; set; }


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
                .HasConversion<string>();

            entity.Property(c => c.Frequency)
                .HasConversion<string>();
        });

        modelBuilder.Entity<ChoreSubmission>(entity =>
        {
            entity.Property(cs => cs.ApprovalStatus)
                .HasConversion<string>();
        });

        // Convert List<DayOfWeek> to a comma-separated string for storage
        modelBuilder.Entity<ParentSettings>(entity =>
        {
            entity.Property(ps => ps.ShopOpeningDays)
                .HasConversion(
                    dow => JsonSerializer.Serialize(dow, (JsonSerializerOptions?)null),
                    dow => JsonSerializer.Deserialize<IReadOnlyList<DayOfWeek>>(dow, (JsonSerializerOptions?)null)!);
        });

        modelBuilder.Entity<ShopItem>(entity =>
        {
            entity.Property(si => si.Status)
                .HasConversion<string>();
        });
    }
}