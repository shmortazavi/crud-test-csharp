using Mc2.CrudTest.Domain.SeedWorks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Infrastructure.SeedWorks
{
    public abstract class BaseWriteUnitOfWork : IWriteUnitOfWork
    {
        private readonly DbContext _context;

        protected BaseWriteUnitOfWork(DbContext context)
        {
            _context = context;
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
