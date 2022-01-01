using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.SeedWorks
{
    public interface IBaseWriteUnitOfWork : IDisposable
    {
        DbContext DbContext();
        Task Commit();
        void Rollback();
    }
}
