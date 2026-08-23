using System.Text.Json;

using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Chore> Chores => Set<Chore>();
    public DbSet<ChoreSubmission> ChoreSubmissions => Set<ChoreSubmission>();
    public DbSet<Kid> Kids => Set<Kid>();
    public DbSet<LoginCode> LoginCodes => Set<LoginCode>();
    public DbSet<Parent> Parents => Set<Parent>();
    public DbSet<ParentSettings> ParentSettings => Set<ParentSettings>();
    public DbSet<ShopItem> ShopItems => Set<ShopItem>();

    public override int SaveChanges()
    {
        AddTimestampsToChangedBaseEntities();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddTimestampsToChangedBaseEntities();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void AddTimestampsToChangedBaseEntities()
    {
        var changedBaseEntities = ChangeTracker
            .Entries()
            .Where(ee => ee is { Entity: EntityBase, State: EntityState.Added or EntityState.Modified });

        var now = DateTime.UtcNow;
        foreach (var baseEntity in changedBaseEntities)
        {
            if (baseEntity.State is EntityState.Added)
            {
                ((EntityBase)baseEntity.Entity).CreatedAt = now;
            }

            ((EntityBase)baseEntity.Entity).UpdatedAt = now;
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>(entity =>
        {
            entity.Property(c => c.Name).HasMaxLength(150);

            entity.Property(c => c.Icon).HasMaxLength(10);

            entity.Property(c => c.Role).HasMaxLength(10).HasConversion<string>();
        });

        builder.Entity<Chore>(entity =>
        {
            entity.Property(c => c.Name).HasMaxLength(150);

            entity.Property(c => c.Icon).HasMaxLength(10);

            entity.Property(c => c.Description).HasMaxLength(300);

            entity.Property(c => c.Difficulty).HasMaxLength(10).HasConversion<string>();

            entity.Property(c => c.Frequency).HasMaxLength(10).HasConversion<string>();
        });

        builder.Entity<ChoreSubmission>(entity =>
        {
            entity.Property(cs => cs.ReviewNotes).HasMaxLength(300);

            entity.Property(cs => cs.ApprovalStatus).HasMaxLength(10).HasConversion<string>();
        });

        builder.Entity<Kid>(entity =>
        {
            // These are both explicit because otherwise EF will find the relationships by convention
            // and create the junction tables for us, which we don't want because we have payload properties on them
            entity.HasMany(k => k.Chores).WithMany(c => c.Kids).UsingEntity<KidChore>();
            entity.HasMany(k => k.ShopItems).WithMany(si => si.Kids).UsingEntity<KidShopItem>();

            // Another explicit relationship to allow PK-to-PK
            entity.HasOne(k => k.LoginCode).WithOne(lc => lc.Kid).HasForeignKey<LoginCode>(lc => lc.KidId).IsRequired();

            entity.Property(k => k.Name).HasMaxLength(100);

            entity.Property(k => k.Avatar).HasMaxLength(10);
        });

        builder.Entity<KidShopItem>(
            entity => entity.Property(ksi => ksi.Status).HasMaxLength(10).HasConversion<string>()
        );

        builder.Entity<LoginCode>(entity =>
        {
            // EF cannot find 'KidId' as a primary key by convention
            entity.HasKey(lc => lc.KidId);

            entity.Property(lc => lc.Code).HasMaxLength(100);
        });

        builder.Entity<Parent>(entity =>
        {
            entity.Property(p => p.FirstName).HasMaxLength(100);

            entity.Property(p => p.LastName).HasMaxLength(100);

            entity.Property(p => p.Email).HasMaxLength(100);

            entity.Property(p => p.Password).HasMaxLength(100);
        });

        builder.Entity<ParentSettings>(entity => entity
                .Property(ps => ps.ShopOpeningDays)
                // Convert List<DayOfWeek> to a comma-separated string for storage
                .HasConversion(
                    dow => JsonSerializer.Serialize(dow, (JsonSerializerOptions?)null),
                    dow => JsonSerializer.Deserialize<IReadOnlyList<DayOfWeek>>(dow, (JsonSerializerOptions?)null)!
                )
        );

        builder.Entity<ShopItem>(entity =>
        {
            entity.Property(si => si.Name).HasMaxLength(50);

            entity.Property(si => si.Icon).HasMaxLength(10);

            entity.Property(si => si.Description).HasMaxLength(300);
        });
    }
}
