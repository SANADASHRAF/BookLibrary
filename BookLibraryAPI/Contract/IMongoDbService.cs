using MongoDB.Driver;

namespace BookLibraryAPI.Contract
{
    public interface IMongoDbService
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
        Task<List<T>> FindAsync<T>(string collectionName, FilterDefinition<T> filter);
        Task InsertOneAsync<T>(string collectionName, T document);
        Task<bool> UpdateOneAsync<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update);
        Task<bool> DeleteOneAsync<T>(string collectionName, FilterDefinition<T> filter);
    }
}
