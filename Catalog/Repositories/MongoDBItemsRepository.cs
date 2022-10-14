using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDBItemsRepository : IItemsRepo
    {
        #region Constant Strings for identifiers
        private const string dbName = "Catalog";
        private const string collectionName = "items";
        #endregion
        private readonly IMongoCollection<Item> _items;
        private readonly FilterDefinitionBuilder<Item> filterBuilder =Builders<Item>.Filter;
        #region Constructor
        public MongoDBItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase db = mongoClient.GetDatabase(dbName);
            _items = db.GetCollection<Item>(collectionName);
        }
        #endregion
        #region Implementation of Interface
        public async Task CreateItemAsync(Item item)
        {
            //We use the MongoDB similar to a static collection
            await _items.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            await _items.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            // Common Filter is built
            var filter = filterBuilder.Eq(item => item.Id, id);
            return await _items.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _items.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter = filterBuilder.Eq(current => current.Id, item.Id);
            await _items.ReplaceOneAsync(filter,item);
        }
        #endregion
    }
}