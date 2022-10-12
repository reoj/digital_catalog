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
        public IEnumerable<Item> GetItems()
        {
            return _items;
        }
        public Item GetItem(Guid id)
        {
            return _items.Where(it => it.Id == id).SingleOrDefault();
        }

        public void CreateItem(Item item)
        {
            _items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            var index = _items.FindIndex(exItem => exItem.Id == item.Id);
            _items[index] = item;
        }
        public void DeleteItem(Guid id)
        {
            var index = _items.FindIndex(exItem => exItem.Id == id);
            _items.RemoveAt(index);
        }
        #endregion
    }
}