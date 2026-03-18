using ChorePoint_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint_API.Repositories
{
    public class ChoreRepository : Repository<Chore>, IChoreRepository
    {

        public ChoreRepository(AppDbContext context) : base(context)
        {
        }
    }
}
