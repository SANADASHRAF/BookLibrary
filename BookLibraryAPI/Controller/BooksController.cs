using AutoMapper;
using BookLibraryAPI.Contract;
using BookLibraryAPI.DTO;
using BookLibraryAPI.Models;
using BookLibraryAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace BookLibraryAPI.Controller
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IbookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        public BooksController(IbookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet (Name = "GetAllBook")]
        public async Task<IActionResult> GetAllBook()
        {
                var books = await _bookRepository.GetAllAsync();

                return Ok(books);
           
        }


        [HttpGet(Name = "GetAuthorBooks")]
        public async Task<IActionResult>GetAuthorBooks(string id)
        {
            var AuthorBook=await _bookRepository.GetAuthorBooks(id);
            return Ok(AuthorBook);  
        }

        [HttpGet(Name = "GetBookById")]
        public async Task<IActionResult> GetBookById(string id)
        {
                var book = await _bookRepository.GetByIdAsync(id);
                if (book == null)
                    return NotFound("الكتاب غير موجود!");
                
                return Ok(book);
        }


        [HttpPost(Name ="CreateBook")]
        public async Task<IActionResult> CreateBook(BookDTO book)
        {
                var author = await _authorRepository.GetByIdAsync(book.AuthorId);
                if (author == null)  
                    return BadRequest("المؤلف غير موجود!");

            var mapper = _mapper.Map<Book>(book);

                await _bookRepository.CreateAsync(mapper);
                return Ok("تم إضافة الكتاب!");
        }


        [HttpPut(Name ="UpdateBook")]
        public async Task<IActionResult> UpdateBook(string id, BookDTO book)
        { 
                var author = await _authorRepository.GetByIdAsync(book.AuthorId);
                if (author == null)
                    return BadRequest("المؤلف غير موجود!");

            var mapper = _mapper.Map<Book>(book);

            await _bookRepository.UpdateAsync(id, mapper);
                return Ok("تم تحديث الكتاب!");
      
        }


        [HttpDelete(Name ="DeleteBook")]
        public async Task<IActionResult> DeleteBook(string id)
        {
                var book = await _bookRepository.GetByIdAsync(id);
                if (book == null)
                    return NotFound("لم يتم العثور على الكتاب!");
                await _bookRepository.DeleteAsync(id);
                return Ok("تم الحذف بنجاح");
        }

    }
}
