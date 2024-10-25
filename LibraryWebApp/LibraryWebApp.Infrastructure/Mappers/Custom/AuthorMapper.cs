using LibraryWebApp.Application.Abstractions.Mappers;
using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain;

namespace LibraryWebApp.Infrastructure.Mappers.Custom
{
    public class AuthorMapper : IAuthorMapper
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookMapper _bookMapper;
        public AuthorMapper(IBookRepository bookRepository, IBookMapper bookMapper)
        {
            _bookRepository = bookRepository;
            _bookMapper = bookMapper;
        }
        public AuthorDTO ToDTO(Author entity)
        {
            var books = entity.Books;
            var booksDTO = new List<BookDTO>();

            foreach (var book in books)
            {
                booksDTO.Add(_bookMapper.ToDTO(book));
            }

            var authorDTO = new AuthorDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                BirthDate = entity.BirthDate,
                Country = entity.Country,
                Books = booksDTO,
            };
            return authorDTO;
        }

        public async Task<Author> ToEntity(AuthorDTO dto)
        {
            var booksDTO = dto.Books;

            var books = new List<Book>();

            var author = new Author(dto.Id, dto.Name, dto.Surname,
                                    dto.BirthDate, dto.Country, books);

            foreach (var bookDTO in booksDTO)
            {
                //try
                //{
                //    var book = await _bookRepository.GetByISBN(bookDTO.ISBN);
                //    author.AddBook(book);
                //    //books.Add(book);
                //}
                //catch (Exception ex)
                //{
                //    var book = new Book(bookDTO.ISBN, bookDTO.Title, bookDTO.Genre, bookDTO.Description, author);

                //    author.AddBook(book);
                //    await _bookRepository.Add(book);
                //    //books.Add(book);
                //}

                var book = new Book(bookDTO.ISBN, bookDTO.Title, bookDTO.Genre, bookDTO.Description, author);

                author.AddBook(book);
                //await _bookRepository.Add(book);
                //books.Add(book);

            }
            //author.Books = books;

            return author;
        }
    }
}
