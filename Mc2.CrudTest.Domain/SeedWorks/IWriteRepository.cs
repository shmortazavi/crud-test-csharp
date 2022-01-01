using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.SeedWorks
{
    public interface IWriteRepository<T> where T : Entity
    {
        T Add(T entity);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
