using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Domain;

namespace LibraryWebApp.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task Add(Book entity)
        {
            await _bookRepository.Add(entity);
        }

        public async Task Delete(string isbn)
        {
            await _bookRepository.Delete(isbn);
        }

        public async Task<List<Book>> GetAll()
        {
            return await _bookRepository.GetAll();
        }

        public async Task<Book> GetByISBN(string isbn)
        {
            return await _bookRepository.GetByISBN(isbn);
        }

        public async Task Update(string isbn, Book entity)
        {
            await _bookRepository.Update(isbn, entity);
        }
    }
}
