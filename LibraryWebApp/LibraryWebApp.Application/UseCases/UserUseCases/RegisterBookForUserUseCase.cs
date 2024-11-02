using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.UserUseCases;
using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.UseCases.UserUseCases
{
    public class RegisterBookForUserUseCase : IRegisterBookForUserUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegisterBookForUserUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task RegisterBookForUser(User user, Book book, DateTime receiptDate, DateTime returnDate)
        {
            await _unitOfWork.Users.RegisterBookForUser(user, book, receiptDate, returnDate);
        }
    }
}
