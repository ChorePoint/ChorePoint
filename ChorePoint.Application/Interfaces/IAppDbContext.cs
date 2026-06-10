using ChorePoint.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Interfaces;

public interface IAppDbContext
{
    public DbSet<Kid> Kids { get; }
    public DbSet<Parent> Parents { get; }
    public DbSet<Chore> Chores { get; }
    public DbSet<ChoreSubmission> ChoreSubmissions { get; }
    public DbSet<ParentSettings> ParentSettings { get; }
    public DbSet<ShopItem> ShopItems { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}