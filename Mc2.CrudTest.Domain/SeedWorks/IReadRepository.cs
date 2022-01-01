using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.SeedWorks
{
    public interface IReadRepository<T> where T : Entity
    {
        Task<T> GetById(Guid id);
        Task<List<T>> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
    }
}
