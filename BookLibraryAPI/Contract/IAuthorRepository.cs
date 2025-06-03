using BookLibraryAPI.DTO;
using BookLibraryAPI.Models;

namespace BookLibraryAPI.Contract
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync();
        Task<ResultAutorWithBooksDto> AuthorsWithBooks();
        Task<Author> GetByIdAsync(string id);
        Task CreateAsync(Author author);
        Task UpdateAsync(string id, Author author);
        Task DeleteAsync(string id);
    }
}
