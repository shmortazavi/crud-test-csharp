using Mc2.CrudTest.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.SeedWorks
{
    public interface IWriteUnitOfWork : IDisposable
    {
        public ICustomerWriteRepository CustomerWriteRepository { get; }
        DbContext DbContext();
        Task Commit();
        void Rollback();

    }
}
