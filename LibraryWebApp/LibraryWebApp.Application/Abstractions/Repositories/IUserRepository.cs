using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.Abstractions.Repositories
{
    public interface IUserRepository : IPagedRepository<User>
    {
        public Task<List<User>> GetAll();
        public Task Add(User entity);
        public Task Update(string login, User entity);
        public Task Delete(string login);
        public Task<User?> GetByLogin(string login);
        public Task RegisterBookForUser(User user, Book book,
            DateTime receiptDate, DateTime returnDate);
    }
}
