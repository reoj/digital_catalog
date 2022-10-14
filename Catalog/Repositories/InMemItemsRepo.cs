using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Models;

namespace Catalog.Repositories
{
    
    public class InMemItemsRepo : IItemsRepo
    {
        #region Fields
        private readonly List<Item> _items = new()
        {
            new Item
            {
                Name = "Blue Potion",
                Price = 9.0M,
                CreatedTime = DateTimeOffset.UtcNow
            },
            new Item
            {
                Name = "Red Potion",
                Price = 12.0M,
                CreatedTime = DateTimeOffset.UtcNow
            },
            new Item
            {
                Name = "Iron Sword",
                Price = 22.5M,
                CreatedTime = DateTimeOffset.UtcNow
            },
            new Item
            {
                Name = "Bronze Shield",
                Price = 18.2M,
                CreatedTime = DateTimeOffset.UtcNow
            }
        };
        #endregion

        #region Public Methods
        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(_items);
        }
        public async Task<Item> GetItemAsync(Guid id)
        {
            var item = _items.Where(it => it.Id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        public async Task CreateItemAsync(Item item)
        {
            _items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = _items.FindIndex(exItem => exItem.Id == item.Id);
            _items[index] = item;
            await Task.CompletedTask;
        }
        public async Task DeleteItemAsync(Guid id)
        {
            var index = _items.FindIndex(exItem => exItem.Id == id);
            _items.RemoveAt(index);
            await Task.CompletedTask;
        }
        #endregion
    }
}