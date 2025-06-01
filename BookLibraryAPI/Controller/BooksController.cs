using AutoMapper;
using BookLibraryAPI.Contract;
using BookLibraryAPI.DTO;
using BookLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPI.Controller
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IbookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IbookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet (Name = "GetAllBook")]
        public async Task<IActionResult> GetAllBook()
        {
            try
            {
                var books = await _bookRepository.GetAllAsync();

                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet(Name = "GetBookById")]
        public async Task<IActionResult> GetBookById(string id)
        {
            try
            {
                var book = await _bookRepository.GetByIdAsync(id);

                if (book == null)
                    return NotFound("لم يتم العثور على الكتاب!");

                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost(Name ="CreateBook")]
        public async Task<IActionResult> CreateBook(BookDTO book)
        {
            try
            {
                var bookMapped = _mapper.Map<Book>(book);
                await _bookRepository.CreateAsync(bookMapped);
                return CreatedAtAction(nameof(GetBookById), new { book });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut(Name ="UpdateBook")]
        public async Task<IActionResult> UpdateBook(string id, BookDTO book)
        {
            try
            {
                var existingBook = await _bookRepository.GetByIdAsync(id);
                if (existingBook == null)
                    return NotFound("لم يتم العثور على الكتاب!");

                var bookMapped = _mapper.Map<Book>(book);
                bookMapped.Id = id;
                
                await _bookRepository.UpdateAsync(id, bookMapped);
                return Ok("تم التعديل بنجاح");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete(Name ="DeleteBook")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            try
            {
                var book = await _bookRepository.GetByIdAsync(id);
                if (book == null)
                    return NotFound("لم يتم العثور على الكتاب!");
                await _bookRepository.DeleteAsync(id);
                return Ok("تم الحذف بنجاح");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
