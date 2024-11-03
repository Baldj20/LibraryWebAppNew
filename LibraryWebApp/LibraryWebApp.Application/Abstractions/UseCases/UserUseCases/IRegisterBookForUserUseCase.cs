using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.Abstractions.UseCases.UserUseCases
{
    public interface IRegisterBookForUserUseCase
    {
        public Task RegisterBookForUser(UserDTO user, BookDTO book, DateTime receiptDate, DateTime returnDate);
    }
}
