using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Models;

namespace Catalog.Repositories
{
   public interface IItemsRepo
    {
        Task<Item> GetItemAsync(Guid id);
        Task <IEnumerable<Item>>GetItemsAsync();
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(Guid id);
    }
}