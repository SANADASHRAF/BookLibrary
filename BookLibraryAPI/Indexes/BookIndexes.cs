using BookLibraryAPI.Models;
using MongoDB.Driver;

namespace BookLibraryAPI.Indexes
{
    public  class BookIndexes
    {
        private IMongoCollection<Book> _books;

        public BookIndexes(IMongoCollection<Book> books) 
        { 
            _books = books;
        }

        public void CreateIndexes()
        {
            // unique index
            var BookNameIndex = Builders<Book>.IndexKeys
                .Ascending(index => index.Name);
            var BookNameIndexModel = new CreateIndexModel<Book>(BookNameIndex, new CreateIndexOptions
            {
                Name = "book_name_index",
                Unique = true,
            });

            //single field index
            var TitleIndex = Builders<Book>.IndexKeys
                .Ascending(index => index.Title);
            var TitleIndexModel = new CreateIndexModel<Book>(TitleIndex, new CreateIndexOptions
            {
                Name = "Title_index"
            });

            //compund index
            var bookCompoundIndex = Builders<Book>.IndexKeys
                .Ascending(book => book.AuthorId)
                .Ascending(book => book.Price);
            var bookCompoundIndexModel = new CreateIndexModel<Book>(bookCompoundIndex, new CreateIndexOptions
            {
                Name = "authorid_price_index"
            });

            _books.Indexes.CreateMany(new[] { BookNameIndexModel, TitleIndexModel, bookCompoundIndexModel });

        }


    }
}
