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
        #region Implementation of interface
        public void CreateItem(Item item)
        {
            //We use the MongoDB similar to a static collection
            _items.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            _items.DeleteOne(filter);
        }

        public Item GetItem(Guid id)
        {
            // Common Filter is built
            var filter = filterBuilder.Eq(item => item.Id, id);
            return _items.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return _items.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            var filter = filterBuilder.Eq(current => current.Id, item.Id);
            _items.ReplaceOne(filter,item);
        }
        #endregion
    }
}