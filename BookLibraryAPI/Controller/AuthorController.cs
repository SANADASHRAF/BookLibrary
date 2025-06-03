using AutoMapper;
using BookLibraryAPI.Contract;
using BookLibraryAPI.DTO;
using BookLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPI.Controller
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthorController : ControllerBase
    {
        private readonly IbookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(IbookRepository bookRepository, IAuthorRepository authorRepository,IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAllAuthor")]
        public async Task<IActionResult> GetAllAuthor()
        {
                var Auth = await _authorRepository.GetAllAsync();
                return Ok(Auth);
        }


        [HttpGet(Name = "GetAllAuthorWithBooks")]
        public async Task<IActionResult> GetAllAuthorWithBooks()
        {
            var Auth = await _authorRepository.AuthorsWithBooks();
            return Ok(Auth);
        }


        [HttpGet(Name = "GetAuthorById")]
        public async Task<IActionResult> GetAuthorById(string id)
        {
                var Auth = await _authorRepository.GetByIdAsync(id);
                if (Auth == null)
                    return NotFound("المولف غير موجود!");

                return Ok(Auth);
        }

        [HttpPost(Name = "CreateAuthor")]
        public async Task<IActionResult> CreateAuthor(AuthorDto Authordto)
        {
            var mapper = _mapper.Map<Author>(Authordto);
            await _authorRepository.CreateAsync(mapper);
            return Ok("تم إضافة المولف!");
        }


        [HttpPut(Name = "UpdateAuthor")]
        public async Task<IActionResult> UpdateAuthor(string id, AuthorDto Authordto)
        {
           
                var author = await _authorRepository.GetByIdAsync(id);
                if (author == null)
                
                    return BadRequest("المؤلف غير موجود!");

                var mapper = _mapper.Map<Author>(Authordto);
                await _authorRepository.UpdateAsync(id, mapper);
                return Ok("تم تحديث المولف!");            
        }


        [HttpDelete(Name = "DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthor(string id)
        {
                var book = await _authorRepository.GetByIdAsync(id);
                if (book == null)
                    return NotFound("لم يتم العثور على المولف!");
                await _authorRepository.DeleteAsync(id);
                return Ok("تم الحذف بنجاح");
            
        }

    }
}
