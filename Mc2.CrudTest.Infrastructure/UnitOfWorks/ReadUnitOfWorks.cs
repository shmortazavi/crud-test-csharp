using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Infrastructure.Repositories.Customers;
using Mc2.CrudTest.Infrastructure.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructure.UnitOfWorks
{
    public class ReadUnitOfWorks : BaseReadUnitOfWork
    {
        private readonly DbContext _context;
        public ReadUnitOfWorks(Mc2DbContext context) : base(context)
        {
            _context = context;
        }

        private readonly ICustomerReadRepository _customerReadRepository;
        public ICustomerReadRepository CustomerReadRepository
        {
            get => _customerReadRepository ?? new CustomerReadRepository(DbContext());
        }
    }
}
