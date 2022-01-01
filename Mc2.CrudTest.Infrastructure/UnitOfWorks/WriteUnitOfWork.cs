using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.SeedWorks;
using Mc2.CrudTest.Infrastructure.Repositories.Customers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Infrastructure.UnitOfWorks
{
    public class WriteUnitOfWork : IWriteUnitOfWork
    {
        private readonly DbContext _context;
        protected WriteUnitOfWork(DbContext context)
        {
            _context = context;
        }


        private readonly ICustomerWriteRepository _customerWriteRepository;
        public ICustomerWriteRepository CustomerWriteRepository
        {
            get => _customerWriteRepository ?? new CustomerWriteRepository(DbContext());
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
        public void Rollback()
        {
            _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
        public DbContext DbContext()
        {
            return _context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }



    }
}
