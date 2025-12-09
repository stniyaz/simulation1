using Hackathon.Core.Entities;
using Hackathon.Core.Interfaces;
using Hackathon.Data.DAL;

namespace Hackathon.Data.Repositories;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context)
    {
    }
}
