using ChorePoint.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChorePoint.Application.Interfaces;

public interface IAppDbContext
{
    DbSet<Category> Categories { get; }
    DbSet<Chore> Chores { get; }
    DbSet<ChoreSubmission> ChoreSubmissions { get; }
    DbSet<Kid> Kids { get; }
    DbSet<LoginCode> LoginCodes { get; }
    DbSet<Parent> Parents { get; }
    DbSet<ParentSettings> ParentSettings { get; }
    DbSet<ShopItem> ShopItems { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    EntityEntry Entry(object entity);
}
