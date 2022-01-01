using Mc2.CrudTest.Domain.Customers;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.SeedWorks
{
    public interface IWriteUnitOfWork : IBaseUnitOfWork
    {
        Task Commit();
        void Rollback();
        public ICustomerWriteRepository CustomerWriteRepository { get; }
    }
}
