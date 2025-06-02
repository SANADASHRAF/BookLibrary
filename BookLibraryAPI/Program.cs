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

            builder.Services.AddApiServices(builder.Configuration);

            var app = builder.Build();

            app.AddMiddleware();

            app.Run();
        }
    }
}