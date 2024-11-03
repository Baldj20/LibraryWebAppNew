using LibraryWebApp.Application;
using LibraryWebApp.Application.Abstractions.UseCases.BookUseCases;
using LibraryWebApp.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IAddBookUseCase _addBookUseCase;
        private readonly IDeleteBookUseCase _deleteBookUseCase;
        private readonly IGetAllBooksUseCase _getAllBooksUseCase;
        private readonly IGetBookByISBNUseCase _getBookByISBNUseCase;
        private readonly IUpdateBookUseCase _updateBookUseCase;
        private readonly IGetPagedBooksUseCase _getPagedBooksUseCase;
        public BookController(IAddBookUseCase addBookUseCase, IDeleteBookUseCase deleteBookUseCase,
            IGetAllBooksUseCase getAllBooksUseCase, IGetBookByISBNUseCase getBookByISBNUseCase,
            IUpdateBookUseCase updateBookUseCase, IGetPagedBooksUseCase getPagedBooksUseCase)
        {
            _addBookUseCase = addBookUseCase;
            _deleteBookUseCase = deleteBookUseCase;
            _getAllBooksUseCase = getAllBooksUseCase;
            _getBookByISBNUseCase = getBookByISBNUseCase;
            _updateBookUseCase = updateBookUseCase;
            _getPagedBooksUseCase = getPagedBooksUseCase;
        }

        [HttpPost]
        public async Task<ActionResult> Add(BookDTO dto)
        {
            await _addBookUseCase.Add(dto);
            return Ok();
        }

        [HttpDelete("{isbn}")]
        public async Task<ActionResult> Delete(string isbn)
        {
            await _deleteBookUseCase.Delete(isbn);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> GetAll()
        {
            var books = await _getAllBooksUseCase.GetAll();
            return Ok(books);
        }

        [HttpGet("{isbn}")]
        public async Task<ActionResult<BookDTO>> GetByISBN(string isbn)
        {
            var book = await _getBookByISBNUseCase.GetByISBN(isbn);
            return Ok(book);
        }

        [HttpPut("{isbn}")]
        public async Task<ActionResult> Update(string isbn, BookDTO dto)
        {
            await _updateBookUseCase.Update(isbn, dto);
            return Ok();
        }

        [HttpGet("{pageNumber}, {pageSize}")]
        public async Task<ActionResult> GetPaged(int pageNumber, int pageSize)
        {
            var result = await _getPagedBooksUseCase.GetPagedBooks(new PaginationParams { PageNumber = pageNumber, PageSize = pageSize });
            return Ok(result);
        }
    }
}
