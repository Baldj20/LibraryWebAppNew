using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Infrastructure.Exceptions;

namespace LibraryWebApp.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Add(BookDTO dto)
        {
            var book = await _unitOfWork._bookMapper.ToEntity(dto);
            var author = await _unitOfWork.Authors.GetById(dto.AuthorId);
            if (author == null)
            {
                throw new NotFoundException("Author not found");
            }
            author.AddBook(book);
            await _unitOfWork.Authors.Update(author.Id, author);                     
        }

        public async Task Delete(string isbn)
        {
            await _unitOfWork.Books.Delete(isbn);
        }

        public async Task<BookDTO> GetByISBN(string isbn)
        {
            var book = await _unitOfWork.Books.GetByISBN(isbn);
            return _unitOfWork._bookMapper.ToDTO(book);
        }

        public async Task Update(string isbn, BookDTO dto)
        {
            var book = await _unitOfWork._bookMapper.ToEntity(dto);

            await _unitOfWork.Books.Update(isbn, book);
        }

        public async Task<List<BookDTO>> GetAll()
        {
            var bookDTOList = new List<BookDTO>();

            var books = await _unitOfWork.Books.GetAll();

            foreach (var book in books)
            {
                bookDTOList.Add(_unitOfWork._bookMapper.ToDTO(book));
            }

            return bookDTOList;
        }
    }
}
