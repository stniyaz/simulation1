using Hackathon.Core.Entities;
using Hackathon.Core.Interfaces;
using Hackathon.Core.Interfaces.Common;
using Hackathon.Data.DAL;
using Hackathon.Data.Repositories.Common;

namespace Hackathon.Data.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context)
    {
    }
}
