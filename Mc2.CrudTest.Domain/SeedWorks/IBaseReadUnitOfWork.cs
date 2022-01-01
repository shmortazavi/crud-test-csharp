using Microsoft.EntityFrameworkCore;
using System;

namespace Mc2.CrudTest.Domain.SeedWorks
{
    public interface IBaseReadUnitOfWork : IDisposable
    {
        DbContext DbContext();
    }
}
