using BookLibraryAPI.Models;

namespace BookLibraryAPI.Contract
{
    public interface IbookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(string id);
        Task CreateAsync(Book book);
        Task UpdateAsync(string id, Book book);
        Task DeleteAsync(string id);


    }
}
