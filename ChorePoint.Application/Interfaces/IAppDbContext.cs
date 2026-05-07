using ChorePoint.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Interfaces;

public interface IAppDbContext
{
    public DbSet<User> Users { get; }
    public DbSet<Parent> Parents { get; }
    public DbSet<Chore> Chores { get; }
    public DbSet<ChoreSubmission> ChoreSubmissions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}