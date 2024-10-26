using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<ActionResult> Add(BookDTO dto)
        {
            await _bookService.Add(dto);
            return Ok();
        }

        [HttpDelete("{isbn}")]
        public async Task<ActionResult> Delete(string isbn)
        {
            await _bookService.Delete(isbn);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> GetAll()
        {
            var books = await _bookService.GetAll();
            return Ok(books);
        }

        [HttpGet("{isbn}")]
        public async Task<ActionResult<BookDTO>> GetByISBN(string isbn)
        {
            var book = await _bookService.GetByISBN(isbn);
            return Ok(book);
        }

        [HttpPut("{isbn}")]
        public async Task<ActionResult> Update(string isbn, BookDTO dto)
        {
            await _bookService.Update(isbn, dto);
            return Ok();
        }
    }
}
