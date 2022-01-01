using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Infrastructure.Repositories.Customers;
using Mc2.CrudTest.Infrastructure.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructure.UnitOfWorks
{
    public class WriteUnitOfWork : BaseWriteUnitOfWork
    {
        public WriteUnitOfWork(DbContext context) : base(context)
        {
        }

        private readonly ICustomerWriteRepository _customerWriteRepository;
        public ICustomerWriteRepository CustomerWriteRepository
        {
            get => _customerWriteRepository ?? new CustomerWriteRepository(DbContext());
        }

    }
}
