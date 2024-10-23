using LibraryWebApp.Domain;
using LibraryWebApp.Domain.Models;

namespace LibraryWebApp.Application.Abstractions.Repositories
{
    public interface IUserBookRepository
    {
        public Task<List<UserBook>> GetAll();
        public Task Add(UserBook entity);
        public Task Update(string isbn, UserBook entity);
        public Task Delete(string isbn);
        public Task<UserBook> GetByISBN(string isbn);
        public Task RegisterBookForUser(User user, Book book, DateTime receiptDate, DateTime returnDate);
    }
}
