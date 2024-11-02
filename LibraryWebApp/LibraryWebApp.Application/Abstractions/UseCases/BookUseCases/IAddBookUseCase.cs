using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.Abstractions.UseCases.BookUseCases
{
    public interface IAddBookUseCase
    {
        public Task Add(BookDTO dto);
    }
}
