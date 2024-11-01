using LibraryWebApp.Domain.Models;
using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.Abstractions.Services
{
    public interface IUserBookService
    {
        public Task<List<UserBook>> GetAll();
        public Task Add(UserBook entity);
        public Task Update(string isbn, UserBook entity);
        public Task Delete(string isbn);
        public Task<UserBook> GetByISBN(string isbn);
    }
}
