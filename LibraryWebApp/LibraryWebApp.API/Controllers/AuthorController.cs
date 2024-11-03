using LibraryWebApp.Application;
using LibraryWebApp.Application.Abstractions.UseCases.AuthorUseCases;
using LibraryWebApp.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.API.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAddAuthorUseCase _addAuthorUseCase;
        private readonly IDeleteAuthorUseCase _deleteAuthorUseCase;
        private readonly IGetAllAuthorsUseCase _getAllAuthorsUseCase;
        private readonly IGetAuthorBooksUseCase _getAuthorBooksUseCase;
        private readonly IGetAuthorByIdUseCase _getAuthorByIdUseCase;
        private readonly IUpdateAuthorUseCase _updateAuthorUseCase;
        private readonly IGetPagedAuthorsUseCase _getPagedAuthorsUseCase;
        public AuthorController(IAddAuthorUseCase addAuthorUseCase, IDeleteAuthorUseCase deleteAuthorUseCase,
            IGetAllAuthorsUseCase getAllAuthorsUseCase, IGetAuthorBooksUseCase getAuthorBooksUseCase, 
            IGetAuthorByIdUseCase getAuthorByIdUseCase, IUpdateAuthorUseCase updateAuthorUseCase, 
            IGetPagedAuthorsUseCase getPagedAuthorsUseCase)
        {
            _addAuthorUseCase = addAuthorUseCase;
            _deleteAuthorUseCase = deleteAuthorUseCase;
            _getAllAuthorsUseCase = getAllAuthorsUseCase;
            _getAuthorBooksUseCase = getAuthorBooksUseCase;
            _getAuthorByIdUseCase = getAuthorByIdUseCase;
            _updateAuthorUseCase = updateAuthorUseCase;
            _getPagedAuthorsUseCase = getPagedAuthorsUseCase;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Add(AuthorDTO dto)
        {
            await _addAuthorUseCase.Add(dto);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _deleteAuthorUseCase.Delete(id);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDTO>>> GetAll()
        {
            var authors = await _getAllAuthorsUseCase.GetAll();
            return Ok(authors);
        }

        [HttpGet("{id}/books")]
        public async Task<ActionResult<List<BookDTO>>> GetBooks([FromRoute(Name = "id")] Guid authorId)
        {
            var books = await _getAuthorBooksUseCase.GetBooks(authorId);
            return Ok(books);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetById([FromRoute(Name = "id")] Guid authorId)
        {
            var author = await _getAuthorByIdUseCase.GetById(authorId);
            return Ok(author);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, AuthorDTO dto)
        {
            await _updateAuthorUseCase.Update(id, dto);
            return Ok();
        }

        [HttpGet("{pageNumber}, {pageSize}")]
        public async Task<ActionResult> GetPaged(int pageNumber, int pageSize)
        {
            var result = await _getPagedAuthorsUseCase.GetPagedAuthors(new PaginationParams { PageNumber = pageNumber, PageSize = pageSize});
            return Ok(result);
        }
    }
}
