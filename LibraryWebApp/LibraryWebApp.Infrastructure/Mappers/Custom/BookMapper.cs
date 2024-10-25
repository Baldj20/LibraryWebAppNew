using LibraryWebApp.Application.Abstractions.Mappers;
using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain;

namespace LibraryWebApp.Infrastructure.Mappers.Custom
{
    public class BookMapper : IBookMapper
    {
        private readonly IAuthorRepository _authorRepository;
        public BookMapper(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public BookDTO ToDTO(Book entity)
        {
            var bookDTO = new BookDTO
            {
                ISBN = entity.ISBN,
                Title = entity.Title,
                Genre = entity.Genre,
                Description = entity.Description,
                AuthorId = entity.Author.Id,
                Count = entity.Count,
            };

            return bookDTO;
        }

        public async Task<Book> ToEntity(BookDTO dto)
        {
            var author = await _authorRepository.GetById(dto.AuthorId);
            var book = new Book(dto.ISBN, dto.Title, dto.Genre, dto.Description, author, dto.Count);
            return book;
        }
    }
}
