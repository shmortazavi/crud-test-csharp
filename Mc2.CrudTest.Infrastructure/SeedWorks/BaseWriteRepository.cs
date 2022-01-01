using Mc2.CrudTest.Domain.SeedWorks;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Infrastructure.SeedWorks
{
    public class BaseWriteRepository<T> : IWriteRepository<T> where T : Entity
    {
        private readonly DbContext _context;

        public BaseWriteRepository(DbContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            return _context.Set<T>().Add(entity).Entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            var result = await _context.Set<T>().AddAsync(entity);
            return result.Entity;
        }

        public T Update(T entity)
        {
            return _context.Set<T>().Update(entity).Entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

    }
}
