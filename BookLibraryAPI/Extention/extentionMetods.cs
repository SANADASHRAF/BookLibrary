using BookLibraryAPI.Contract;
using BookLibraryAPI.middlewares;
using BookLibraryAPI.Models;
using BookLibraryAPI.Repository;
using MongoDB.Driver;

namespace BookLibraryAPI.Extention
{
    public static class extentionMetods 
    {
        public static IServiceCollection AddApiServices (this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BookLibraryDatabaseSettings>(
                configuration.GetSection("BookLibraryDatabaseSettings"));


            services.AddSingleton<IMongoClient>(sp =>
                new MongoClient(configuration.GetSection("BookLibraryDatabaseSettings:ConnectionString").Value));

            services.AddSingleton<IMongoDbService, MongoDbService>();

            services.AddScoped<IbookRepository, bookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();

            services.AddLogging();

            services.AddAutoMapper(typeof(MappingProfillle));

            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddAuthorization();

            services.AddCors(x =>
            {
                x.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });
            return services;
        }

       
        public static WebApplication AddMiddleware(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }

      


    }
}
