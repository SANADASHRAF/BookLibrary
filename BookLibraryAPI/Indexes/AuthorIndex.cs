using BookLibraryAPI.Models;
using MongoDB.Driver;

namespace BookLibraryAPI.Indexes
{
    public class AuthorIndex
    {
        private readonly IMongoCollection<Author> _author;

        public AuthorIndex(IMongoCollection<Author> author)
        {
            _author = author;
        }

        public void CreatIndexes()
        {
            var AuthorNameIndex=Builders<Author>.IndexKeys.Ascending(index=>index.Name);
            var AuthorNameIndexModel=new CreateIndexModel<Author>(AuthorNameIndex,new CreateIndexOptions
            {
                Name ="book_name_index",
                Unique=true,
            });
        }

    }
}
