using System;

namespace Mc2.CrudTest.Domain.SeedWorks
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
