using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.Abstractions.Repositories
{
    public interface IBookRepository : IPagedRepository<Book>
    {
        public Task<List<Book>> GetAll();
        public Task Add(Book entity);
        public Task Update(string isbn, Book entity);
        public Task Delete(string isbn);
        public Task<Book> GetByISBN(string isbn);
    }
}
