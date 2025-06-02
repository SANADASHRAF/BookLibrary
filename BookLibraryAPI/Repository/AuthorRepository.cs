using BookLibraryAPI.Contract;
using BookLibraryAPI.Models;
using MongoDB.Driver;

namespace BookLibraryAPI.Repository
{
    public class AuthorRepository: IAuthorRepository
    {
        private readonly IMongoDbService _mongoDbService;
        private const string CollectionName = "Author";

        public AuthorRepository (IMongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        public async Task<List<Author>> GetAllAsync()
        {

            var filte = Builders<Author>.Filter.Empty;
            return await _mongoDbService.FindAsync(CollectionName, filte);

        }

     
        public async Task<Author> GetByIdAsync(string id)
        {
            var filter = Builders<Author>.Filter.Eq(a => a.Id, id);
            var result = await _mongoDbService.FindAsync(CollectionName, filter);
            return result.FirstOrDefault();
        }


        public async Task CreateAsync(Author author)
        {
             await _mongoDbService.InsertOneAsync(CollectionName,author);
        }


        public async Task UpdateAsync(string id, Author author)
        {
            var filter = Builders<Author>.Filter.Eq(a => a.Id, id);

            var update = Builders<Author>.Update
                .Set(a => a.Name, author.Name);

            var result = await _mongoDbService.UpdateOneAsync(CollectionName, filter, update);

            if (!result)
                throw new Exception("لم يتم العثور على المؤلف لتحديثه!");
            
        }


        public async Task DeleteAsync(string id)
        {
            var filter = Builders<Author>.Filter.Eq(a => a.Id, id);
            var result = await _mongoDbService.DeleteOneAsync(CollectionName, filter);
            if (!result)
                throw new Exception("لم يتم العثور على المؤلف لحذفه!");
        }


    }
}
