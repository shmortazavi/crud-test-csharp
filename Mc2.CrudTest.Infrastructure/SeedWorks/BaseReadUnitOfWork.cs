using Mc2.CrudTest.Domain.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructure.SeedWorks
{
    public abstract class BaseReadUnitOfWork : IReadUnitOfWork
    {
        private readonly DbContext _context;

        public BaseReadUnitOfWork(DbContext context)
        {
            _context = context;
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
