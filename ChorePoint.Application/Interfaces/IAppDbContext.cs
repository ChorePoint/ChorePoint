using ChorePoint.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Interfaces;

public interface IAppDbContext
{
    DbSet<Kid> Kids { get; }
    DbSet<Parent> Parents { get; }
    DbSet<Chore> Chores { get; }
    DbSet<ChoreSubmission> ChoreSubmissions { get; }
    DbSet<ParentSettings> ParentSettings { get; }
    DbSet<ShopItem> ShopItems { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
