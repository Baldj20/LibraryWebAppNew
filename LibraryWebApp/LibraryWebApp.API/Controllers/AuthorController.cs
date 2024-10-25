using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.API.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost]
        public async Task<ActionResult> Add(AuthorDTO dto)
        {
            await _authorService.Add(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _authorService.Delete(id);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDTO>>> GetAll()
        {
            var authors = await _authorService.GetAll();
            return Ok(authors);
        }

        [HttpGet("{id}/books")]
        public async Task<ActionResult<List<BookDTO>>> GetBooks([FromRoute(Name = "id")] Guid authorId)
        {
            var books = await _authorService.GetBooks(authorId);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetById([FromRoute(Name = "id")] Guid authorId)
        {
            var author = await _authorService.GetById(authorId);
            return Ok(author);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, AuthorDTO dto)
        {
            await _authorService.Update(id, dto);
            return Ok();
        }
    }
}
