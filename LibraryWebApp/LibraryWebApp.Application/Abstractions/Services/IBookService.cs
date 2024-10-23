using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.Abstractions.Services
{
    public interface IBookService
    {
        public Task<List<Book>> GetAll();
        public Task Add(Book entity);
        public Task Update(string isbn, Book entity);
        public Task Delete(string isbn);
        public Task<Book> GetByISBN(string isbn);
    }
}
