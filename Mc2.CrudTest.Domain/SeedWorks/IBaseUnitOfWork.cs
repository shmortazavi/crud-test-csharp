using Microsoft.EntityFrameworkCore;
using System;

namespace Mc2.CrudTest.Domain.SeedWorks
{
    public interface IBaseUnitOfWork : IDisposable
    {
        DbContext DbContext();
    }
}
