using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Models
{
    //Inmutable Item Class as Record
    public record Item
    {
        public Guid Id { get ; init; } = Guid.NewGuid();
        public string Name { get ; init; } = "Out of Stock";
        public decimal Price { get ; init; }
        public DateTimeOffset CreatedTime { get ; init; } = DateTimeOffset.UtcNow;
    }
}