using AutoMapper;
using BookLibraryAPI.Contract;
using BookLibraryAPI.DTO;
using BookLibraryAPI.Indexes;
using BookLibraryAPI.Models;
using MongoDB.Driver;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookLibraryAPI.Repository
{
    public class AuthorRepository: IAuthorRepository
    {
        private readonly IMongoDbService _mongoDbService;
        private readonly IMapper _mapper;
       private const string CollectionName = "Author";
       private const string BookCollectionName = "Books";

        public AuthorRepository (IMongoDbService mongoDbService , IMapper mapper)
        {
            _mongoDbService = mongoDbService;
            _mapper = mapper;
        }

        public async Task<List<Author>> GetAllAsync()
        {

            var filte = Builders<Author>.Filter.Empty;
            return await _mongoDbService.FindAsync(CollectionName, filte);

        }

        public async Task<ResultAutorWithBooksDto> AuthorsWithBooks()
        {

           var AutherResult= await _mongoDbService.FindAsync(CollectionName, Builders<Author>.Filter.Empty);
           var BookResult= await _mongoDbService.FindAsync(BookCollectionName, Builders<Book>.Filter.Empty);

            var resultList = new List<AutorWithBooksDto>();

            foreach (var author in AutherResult)
            {
                var AuthMapper = _mapper.Map<AutorWithBooksDto>(author);

                var authorBooks = BookResult.Where(b => b.AuthorId == author.Id).ToList();

                var BookMapper = _mapper.Map<List<BookDto>>(authorBooks);

                foreach (var b in BookMapper)
                {
                    b.Author = author.Name;
                }

                AuthMapper.bookDtos = BookMapper;

                resultList.Add(AuthMapper);
            }

            return new ResultAutorWithBooksDto
            {
                autorWithBooksDtos = resultList
            };

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
