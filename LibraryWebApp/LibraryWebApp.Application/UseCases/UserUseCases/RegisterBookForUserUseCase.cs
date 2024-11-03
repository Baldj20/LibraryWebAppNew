using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.UserUseCases;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Application.Exceptions;

namespace LibraryWebApp.Application.UseCases.UserUseCases
{
    public class RegisterBookForUserUseCase : IRegisterBookForUserUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegisterBookForUserUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task RegisterBookForUser(UserDTO userDTO, BookDTO bookDTO, DateTime receiptDate, DateTime returnDate)
        {
            var book = await _unitOfWork._bookMapper.ToEntity(bookDTO);
            if (book.Count == 0) throw new BookIsOutException();
            var user = await _unitOfWork._userMapper.ToEntity(userDTO);
            await _unitOfWork.Users.RegisterBookForUser(user, book, receiptDate, returnDate);
        }
    }
}
