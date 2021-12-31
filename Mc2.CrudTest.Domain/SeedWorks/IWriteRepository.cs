using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.SeedWorks
{
    public interface IWriteRepository<T> where T : Entity
    {
        Task<T> Insert(T entity);
        Task Update(T entity);
        Task<bool> Delete(T entity);
    }
}
