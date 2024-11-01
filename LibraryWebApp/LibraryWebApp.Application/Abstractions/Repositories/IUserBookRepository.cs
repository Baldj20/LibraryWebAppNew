using LibraryWebApp.Domain.Models;

namespace LibraryWebApp.Application.Abstractions.Repositories
{
    public interface IUserBookRepository : IPagedRepository<UserBook>
    {
        public Task<List<UserBook>> GetAll();
        public Task Add(UserBook entity);
        public Task Update(string isbn, UserBook entity);
        public Task Delete(string isbn);
        public Task<UserBook> GetByISBN(string isbn);
    }
}
