using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Models;

namespace Catalog.Repositories
{
   public interface IItemsRepo
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
    }
}