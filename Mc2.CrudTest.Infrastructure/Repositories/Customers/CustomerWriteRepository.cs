using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Infrastructure.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructure.Repositories.Customers
{
    public class CustomerWriteRepository : BaseWriteRepository<Customer>, ICustomerWriteRepository
    {
        public CustomerWriteRepository(DbContext context) : base(context)
        {
        }

    }
}
