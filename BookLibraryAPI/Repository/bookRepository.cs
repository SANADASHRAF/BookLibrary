using BookLibraryAPI.Contract;
using BookLibraryAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookLibraryAPI.Repository
{
    public class bookRepository : IbookRepository
    {

        private readonly IMongoCollection<Book> _booksCollection;

        public bookRepository(IOptions<BookLibraryDatabaseSettings> bookLibraryDatabaseSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(bookLibraryDatabaseSettings.Value.DatabaseName);
            _booksCollection = database.GetCollection<Book>(bookLibraryDatabaseSettings.Value.BooksCollectionName);
        }

        public async Task<List<Book>> GetAllAsync()=>
           await _booksCollection.Find(book => true).ToListAsync();
        

        public async Task<Book> GetByIdAsync(string id) =>
             await _booksCollection.Find(book => book.Id == id).FirstOrDefaultAsync();
        

        public async Task CreateAsync(Book book) =>
            await _booksCollection.InsertOneAsync(book);
        

        public async Task UpdateAsync(string id, Book book) =>
            await _booksCollection.ReplaceOneAsync(b => b.Id == id, book);
        

        public async Task DeleteAsync(string id) =>
            await _booksCollection.DeleteOneAsync(book => book.Id == id);


        
    }

}

