using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.Abstractions.UseCases.UserUseCases
{
    public interface IRegisterBookForUserUseCase
    {
        public Task RegisterBookForUser(User user, Book book, DateTime receiptDate, DateTime returnDate);
    }
}
