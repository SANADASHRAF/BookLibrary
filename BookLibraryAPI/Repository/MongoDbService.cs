using BookLibraryAPI.Contract;
using BookLibraryAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookLibraryAPI.Repository
{
    public class MongoDbService : IMongoDbService
    {

        private readonly IMongoDatabase _database;
        private readonly BookLibraryDatabaseSettings _settings;

        public MongoDbService(IMongoClient client, IOptions<BookLibraryDatabaseSettings> settings)
        {
            _settings = settings.Value;
            _database = client.GetDatabase(_settings.DatabaseName);
        }


        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            if (!_settings.Collections.ContainsKey(collectionName))
            {
                throw new ArgumentException($"Collection {collectionName} غير موجود في الإعدادات!");
            }
            return _database.GetCollection<T>(_settings.Collections[collectionName]);
        }


        public async Task<List<T>> FindAsync<T>(string collectionName, FilterDefinition<T> filter)
        {
            var collection = GetCollection<T>(collectionName);
            return await collection.Find(filter).ToListAsync();
        }


        public async Task InsertOneAsync<T>(string collectionName, T document)
        {
            var collection = GetCollection<T>(collectionName);
            await collection.InsertOneAsync(document);
        }

        public async Task<bool> UpdateOneAsync<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            var collection = GetCollection<T>(collectionName);
            var result = await collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }


        public async Task<bool> DeleteOneAsync<T>(string collectionName, FilterDefinition<T> filter)
        {
            var collection = GetCollection<T>(collectionName);
            var result = await collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

    }
}
