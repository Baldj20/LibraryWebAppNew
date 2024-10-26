using LibraryWebApp.Application.Abstractions.Mappers;
using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Infrastructure.Exceptions;

namespace LibraryWebApp.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookMapper _bookMapper;
        public BookService(IBookRepository bookRepository, 
            IAuthorRepository authorRepository, IBookMapper bookMapper)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _bookMapper = bookMapper;
        }
        public async Task Add(BookDTO dto)
        {
            var book = await _bookMapper.ToEntity(dto);
            var author = await _authorRepository.GetById(dto.AuthorId);
            if (author == null)
            {
                throw new NotFoundException("Author not found");
            }
            author.AddBook(book);
            await _authorRepository.Update(author.Id, author);                     
        }

        public async Task Delete(string isbn)
        {
            await _bookRepository.Delete(isbn);
        }

        public async Task<BookDTO> GetByISBN(string isbn)
        {
            var book = await _bookRepository.GetByISBN(isbn);
            return _bookMapper.ToDTO(book);
        }

        public async Task Update(string isbn, BookDTO dto)
        {
            var book = await _bookMapper.ToEntity(dto);

            await _bookRepository.Update(isbn, book);
        }

        public async Task<List<BookDTO>> GetAll()
        {
            var bookDTOList = new List<BookDTO>();

            var books = await _bookRepository.GetAll();

            foreach (var book in books)
            {
                bookDTOList.Add(_bookMapper.ToDTO(book));
            }

            return bookDTOList;
        }
    }
}
