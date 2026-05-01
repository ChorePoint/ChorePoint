using ChorePoint.API.Models;

namespace ChorePoint.API.Repositories
{
    public class ChoreRepository : Repository<Chore>, IChoreRepository
    {

        public ChoreRepository(AppDbContext context) : base(context)
        {

        }
    }
}
