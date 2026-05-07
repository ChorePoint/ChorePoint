using ChorePoint.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Interfaces;

public interface IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Chore> Chores { get; set; }
    public DbSet<ChoreSubmission> ChoreSubmissions { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}