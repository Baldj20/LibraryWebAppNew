using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Domain.Models;

namespace LibraryWebApp.Infrastructure.Services
{
    public class UserBookService : IUserBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserBookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Add(UserBook entity)
        {
            await _unitOfWork.UserBooks.Add(entity);
        }

        public async Task Delete(string isbn)
        {
            await _unitOfWork.UserBooks.Delete(isbn);
        }

        public async Task<List<UserBook>> GetAll()
        {
            return await _unitOfWork.UserBooks.GetAll();
        }

        public async Task<UserBook> GetByISBN(string isbn)
        {
            return await _unitOfWork.UserBooks.GetByISBN(isbn);
        }

        public async Task Update(string isbn, UserBook entity)
        {
            await _unitOfWork.UserBooks.Update(isbn, entity);
        }
    }
}
