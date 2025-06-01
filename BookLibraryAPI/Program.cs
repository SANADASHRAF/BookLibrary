using BookLibraryAPI.Contract;
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

            // MongoDB configuration
            builder.Services.Configure<BookLibraryDatabaseSettings>(
                builder.Configuration.GetSection("BookLibraryDatabaseSettings"));

            builder.Services.AddSingleton<IMongoClient>(sp =>
                new MongoClient(builder.Configuration.GetSection("BookLibraryDatabaseSettings:ConnectionString").Value));

            builder.Services.AddSingleton<IbookRepository, bookRepository>();

            builder.Services.AddAutoMapper(typeof(MappingProfillle));

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}