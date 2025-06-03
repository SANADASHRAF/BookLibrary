using BookLibraryAPI.Contract;
using BookLibraryAPI.Extention;
using BookLibraryAPI.middlewares;
using BookLibraryAPI.Models;
using BookLibraryAPI.Repository;
using MongoDB.Driver;

namespace BookLibraryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            CreatIndexes();

            builder.Services.AddApiServices(builder.Configuration);

            var app = builder.Build();

            app.AddMiddleware();

            app.Run();
          
    }

        public static void CreatIndexes()
        {
            var AuthorNameIndex = Builders<Author>.IndexKeys.Ascending(index => index.Name);
            var AuthorNameIndexModel = new CreateIndexModel<Author>(AuthorNameIndex, new CreateIndexOptions
            {
                Name = "book_name_index",
                Unique = true,
            });
        }

    }
}