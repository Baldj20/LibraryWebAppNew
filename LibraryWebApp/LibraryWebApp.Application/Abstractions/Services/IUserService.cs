using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.Abstractions.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetAll();
        public Task Add(UserDTO dto);
        public Task Update(string login, UserDTO dto);
        public Task Delete(string login);
        public Task<TokenDTO> Register(UserDTO dto);
        public Task<User?> GetByLogin(string login);
        public Task<TokenDTO> Authorize(UserDTO dto);
        public Task RegisterBookForUser(User user, Book book, DateTime receiptDate, DateTime returnDate);

    }
}
