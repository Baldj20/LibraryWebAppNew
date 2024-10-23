using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Domain;
using LibraryWebApp.Domain.Models;

namespace LibraryWebApp.Infrastructure.Services
{
    public class UserBookService : IUserBookService
    {
        private readonly IUserBookRepository _userBookRepository;
        public UserBookService(IUserBookRepository userBookRepository)
        {
            _userBookRepository = userBookRepository;
        }
        public async Task Add(UserBook entity)
        {
            await _userBookRepository.Add(entity);
        }

        public async Task Delete(string isbn)
        {
            await _userBookRepository.Delete(isbn);
        }

        public async Task<List<UserBook>> GetAll()
        {
            return await _userBookRepository.GetAll();
        }

        public async Task<UserBook> GetByISBN(string isbn)
        {
            return await _userBookRepository.GetByISBN(isbn);
        }

        public async Task RegisterBookForUser(User user, Book book, DateTime receiptDate, DateTime returnDate)
        {
            await _userBookRepository.RegisterBookForUser(user, book, receiptDate, returnDate); 
        }

        public async Task Update(string isbn, UserBook entity)
        {
            await _userBookRepository.Update(isbn, entity);
        }
    }
}
