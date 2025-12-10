using Hackathon.Core.Entities;
using Hackathon.Core.Interfaces;
using Hackathon.Data.DAL;
using Hackathon.Data.Repositories.Common;

namespace Hackathon.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
