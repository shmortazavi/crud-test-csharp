using Mc2.CrudTest.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using System;

namespace Mc2.CrudTest.Domain.SeedWorks
{
    public interface IReadUnitOfWork : IDisposable
    {
        public ICustomerReadRepository CustomerReadRepository { get; }
        DbContext DbContext();
    }
}
