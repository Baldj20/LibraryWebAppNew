using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.Abstractions.UseCases.BookUseCases
{
    public interface IUpdateBookUseCase
    {
        public Task Update(string isbn, BookDTO dto);
    }
}
