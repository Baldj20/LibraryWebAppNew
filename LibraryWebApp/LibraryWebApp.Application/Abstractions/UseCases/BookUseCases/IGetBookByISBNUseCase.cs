using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.Abstractions.UseCases.BookUseCases
{
    public interface IGetBookByISBNUseCase
    {
        public Task<BookDTO> GetByISBN(string isbn);
    }
}
