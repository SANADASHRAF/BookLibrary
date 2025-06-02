using BookLibraryAPI.Contract;
using BookLibraryAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookLibraryAPI.Repository
{
    public class bookRepository : IbookRepository
    {

        private readonly IMongoDbService _mongoDbService;
        private const string CollectionName = "Books";

        public bookRepository(IMongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            var filter = Builders<Book>.Filter.Empty;
            return await _mongoDbService.FindAsync(CollectionName, filter);
        }

        public async Task<Book> GetByIdAsync(string id)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Id, id);
            var result = await _mongoDbService.FindAsync(CollectionName, filter);
            return result.FirstOrDefault();
        }

        public async Task CreateAsync(Book book)
        {
            await _mongoDbService.InsertOneAsync(CollectionName, book);
        }

        public async Task UpdateAsync(string id, Book book)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Id, id);
            var update = Builders<Book>.Update
                .Set(b => b.Name, book.Name)
                .Set(b => b.Title, book.Title)
                .Set(b => b.AuthorId, book.AuthorId)
                .Set(b => b.Price, book.Price);
            var result = await _mongoDbService.UpdateOneAsync(CollectionName, filter, update);
            if (!result)
            {
                throw new Exception("لم يتم العثور على الكتاب لتحديثه!");
            }
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Id, id);
            var result = await _mongoDbService.DeleteOneAsync(CollectionName, filter);
            if (!result)
            {
                throw new Exception("لم يتم العثور على الكتاب لحذفه!");
            }
        }

    }

}

